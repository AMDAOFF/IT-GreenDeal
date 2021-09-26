from imutils.video import VideoStream
import imutils
import face_recognition
import pickle
import time
import cv2

print("Loading model.")

# Load the data from the model.
data = pickle.loads(open("model.pickle", "rb").read())

print("Starting the webcam.")

# Initialize the webcam.
vs = VideoStream().start()

# Allow the webcam to warm up.
time.sleep(2)

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

	names = []

    # Loop over the facial embeddings.
	for encoding in encodings:

		# Attempt to match each face from the webcam stream to our known encodings. (Adjust tolerance for stricter matching.)
		matches = face_recognition.compare_faces(data["encodings"],	encoding, tolerance=0.4)
		name = "Unknown"

		# Check to see if we have found a match.
		if True in matches:

			# Find the indexes of all matched faces from the model.
			matchedIds = [i for (i, b) in enumerate(matches) if b]

			# Count the total number of times each face was matched.
			counts = {}

			# Loop over the matched indexes and maintain a count for each recognized face.
			for i in matchedIds:
				name = data["names"][i]
				counts[name] = counts.get(name, 0) + 1

			# Determine the recognized face with the largest number of votes.
			# E.g. If the person in the webcam frame gets matched with 2 different persons from the model then
			# select the name that matched with most of the trained images.
			name = max(counts, key=counts.get)
		
		# Update the list of names.
		names.append(name)

    # Loop over the recognized faces.
	for ((top, right, bottom, left), name) in zip(boxes, names):

		# Rescale the face coordinates.
		top = int(top * ratio)
		right = int(right * ratio)
		bottom = int(bottom * ratio)
		left = int(left * ratio)

		# Draw a box around the detected face.
		cv2.rectangle(frame, (left, top), (right, bottom), (0, 0, 255), 2)

		# If the face is at the top of the screen, put the name label inside the drawn box.
		y = top - 15 if top - 15 > 15 else top + 15

		# Put the name below the box.
		cv2.putText(frame, name, (left, y), cv2.FONT_HERSHEY_SIMPLEX, 0.75, (0, 0, 255), 2)

	# Display the webcam frame.
	cv2.imshow("Attendance", frame)

	# If "q" is pressed terminate program.
	key = cv2.waitKey(1)	
	if key == ord("q"):
		break

cv2.destroyAllWindows()
vs.stop()