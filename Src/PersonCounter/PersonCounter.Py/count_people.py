from os.path import exists
import face_recognition
import time
import sys
import argparse
import cv2
import pymssql
import pika
from datetime import datetime


#TODO: Add Try Catches - Rabbit MQ, Webcam, SQL
#TODO: Implement DB Con

# Add arguments
ap = argparse.ArgumentParser()
ap.add_argument("-ip", "--ip", type=str, default="192.168.0.4", help="The camera's IP Address")
ap.add_argument("-u", "--username", type=str, default=None, help="The username used to login to the camera")
ap.add_argument("-p", "--password", type=str, default=None, help="The password used to login to the camera")
args = vars(ap.parse_args())

######## Real implementation: ########
# if args["username"] is not None and args["password"] is not None:
#     vs = cv2.VideoCapture(f"rtsp://{args['username']}:{args['password']}@{args['ip']}/video")
# else:
#     vs = cv2.VideoCapture(f"http://{args['ip']}/video")

######## POC: ########
vs = cv2.VideoCapture(0)

# Allow the webcam to warm up.
time.sleep(2)

personsInRoom = []

iteration = 0
while iteration < 10:
    iteration += 1
    success, img = vs.read()
    faces = face_recognition.face_locations(img)
    totalFaces = len(faces)
    personsInRoom.append(totalFaces)
    time.sleep(0.5)

personCounter = max(set(personsInRoom), key=personsInRoom.count)
previousPersonCounter = None
cameraIP = args['ip']

if exists(f"Files/{cameraIP}.txt"):
    with open(f"Files/{cameraIP}.txt", "rb") as file:
        previousPersonCounter = file.readline()

if personCounter != int(previousPersonCounter):
    print(f"{args['ip']};{personCounter}")
    sys.stdout.flush()
    with open(f'Files/{cameraIP}.txt', "w") as file:
        file.write(f"{personCounter}")

    connection = pika.BlockingConnection(
        pika.ConnectionParameters(host='localhost', port=5672))
    channel = connection.channel()

    channel.queue_declare(queue='room update')
    
    channel.basic_publish(exchange='', routing_key='room update', body=f'{"Room ID"};{personCounter};{datetime.now().strftime("%d/%m/%Y %H:%M:%S")}')
    connection.close()