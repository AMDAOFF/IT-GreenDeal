from imutils import paths
import face_recognition
import pickle
import cv2
import os

# Get the path to the input images for the model.
print("Fetching image paths.")
imagePaths = list(paths.list_images("ML Images"))

knownEncodings = []
knownNames = []

# Loop over the image paths.
for (i, imagePath) in enumerate(imagePaths):

	print("Processing image {}/{}".format(i + 1, len(imagePaths)))

	# Extract the person name from the image path. ([-2] Gets the second last item in the array.)
	name = imagePath.split(os.path.sep)[-2]

	# Load the input image.
	image = cv2.imread(imagePath)

	# Convert from cv2's BGR to dlib's RGB.
	imageRGB = cv2.cvtColor(image, cv2.COLOR_BGR2RGB)

	# "hog" = LESS accuracy MORE speed.
	# "cnn" = MORE accuracy LESS speed.
	# Detect the (x, y)-coordinates for the face.
	boxes = face_recognition.face_locations(imageRGB, model="hog")
	
	# Get the facial encodings for the face.
	encodings = face_recognition.face_encodings(imageRGB, boxes)

	# Append the name and encoded face.
	for encoding in encodings:
		knownEncodings.append(encoding)
		knownNames.append(name)

data = {"encodings": knownEncodings, "ids": knownNames}

with open("model2.pickle", "wb") as file:
	file.write(pickle.dumps(data))

print("Sucessfully created the new model")