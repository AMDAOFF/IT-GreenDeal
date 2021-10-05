from imutils.video import VideoStream
import imutils
import face_recognition
import pickle
import time
import cv2
import pika
import argparse

# construct the argument parser and parse the arguments
ap = argparse.ArgumentParser()
ap.add_argument("-ip", "--ip", type=str, required=True, help="The camera's IP Address")
args = vars(ap.parse_args())

cameraIP = args['ip']

print("Loading model.")

# Load the data from the model.
data = pickle.loads(open("C:\\Users\\jimmy\\source\\repos\\IT-GreenDeal\\Src\\AbsenceSystem\\Absence.Py\\model.pickle", "rb").read())

print(f"Starting the webcam with the IP: {cameraIP}")

# Initialize the webcam.
vs = VideoStream(0).start()

# Allow the webcam to warm up.
time.sleep(2)

alreadyMatchedIds = []
alreadyMatchedIds.append(None)
# Loop over frames from the webcam.
while True:

	# Get the frame from the webcam.
	frame = vs.read()

	# Convert from cv2's BGR to dlib's RGB.
	imageRGB = cv2.cvtColor(frame, cv2.COLOR_BGR2RGB)

	# Resize frame to have a width of 750px. (to speedup processing.)
	frame = imutils.resize(frame, width=750)

	# Get the frame ratio so we can rescale back later.
	ratio = frame.shape[1] / float(imageRGB.shape[1])

	# "hog" = LESS accuracy MORE speed.
	# "cnn" = MORE accuracy LESS speed.
	# Detect the (x, y)-coordinates for the faces.
	boxes = face_recognition.face_locations(imageRGB, model="cnn")
	
	# Get the facial encodings for the faces.
	encodings = face_recognition.face_encodings(imageRGB, boxes)

	ids = []

    # Loop over the facial encodings.
	for encoding in encodings:

		# Attempt to match each face from the webcam stream to our known encodings. (Adjust tolerance for stricter matching.)
		matches = face_recognition.compare_faces(data["encodings"],	encoding, tolerance=0.5)
		id = None

		# Check to see if we have found a match.
		if True in matches:

			# Find the indexes of all matched faces from the model.
			matchedIds = [i for (i, b) in enumerate(matches) if b]

			# Count the total number of times each face was matched.
			counts = {}

			# Loop over the matched indexes and maintain a count for each recognized face.
			for i in matchedIds:
				id = data["ids"][i]
				counts[id] = counts.get(id, 0) + 1

			# Determine the recognized face with the largest number of votes.
			# E.g. If the person in the webcam frame gets matched with 2 different persons from the model then
			# select the id that matched with most of the trained images.
			id = max(counts, key=counts.get)
		
		# Update the list of ids.
		ids.append(id)
		if not id in alreadyMatchedIds:
			alreadyMatchedIds.append(id)
			credentials = pika.PlainCredentials('guest', 'guest')
			connection = pika.BlockingConnection(pika.ConnectionParameters(host='localhost', port=5672, virtual_host='/', credentials=credentials))
			channel = connection.channel()
			channel.queue_declare(queue=f"Absence-{cameraIP}", durable=True)
			channel.start_consuming()
			channel.basic_publish(exchange='', routing_key=f'Absence-{cameraIP}', body=f'{id}')
			print(f"Succeded")

    # Loop over the recognized faces.
	for ((top, right, bottom, left), id) in zip(boxes, ids):

		# Rescale the face coordinates.
		top = int(top * ratio)
		right = int(right * ratio)
		bottom = int(bottom * ratio)
		left = int(left * ratio)

		# Draw a box around the detected face.
		cv2.rectangle(frame, (left, top), (right, bottom), (0, 0, 255), 2)

		# If the face is at the top of the screen, put the id label inside the drawn box.
		y = top - 15 if top - 15 > 15 else top + 15

		# Put the id below the box.
		cv2.putText(frame, id, (left, y), cv2.FONT_HERSHEY_SIMPLEX, 0.75, (0, 0, 255), 2)

	# Display the webcam frame.
	cv2.imshow("Attendance", frame)

	# If "q" is pressed terminate program.
	key = cv2.waitKey(1)	
	if key == ord("q"):
		break

cv2.destroyAllWindows()
vs.stop()