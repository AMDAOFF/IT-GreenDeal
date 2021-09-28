from os.path import exists
import face_recognition
import time
from imutils.video import VideoStream
import sys

# Initialize the webcam.
vs = VideoStream().start()

# Allow the webcam to warm up.
time.sleep(2)

personsInRoom = []

iteration = 0
while iteration < 10:
  iteration += 1
  img = vs.read()
  faces = face_recognition.face_locations(img)
  totalFaces = len(faces)
  personsInRoom.append(totalFaces)
  time.sleep(0.5)

personCounter = max(set(personsInRoom), key = personsInRoom.count)
previousPersonCounter = None

if exists('persons.txt'):
    with open("persons.txt", "rb") as file:
        previousPersonCounter = file.read()

if personCounter != previousPersonCounter:
    print(f"Persons: {personCounter}")
    sys.stdout.flush()
    with open('persons.txt', "w") as file:
        file.write(f"{personCounter}")		
        #TODO: Send Rabbit MQ to room update queue
