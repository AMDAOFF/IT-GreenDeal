from os.path import exists
from datetime import datetime
import face_recognition
import time
import sys
import cv2
import pika
import pyodbc

try:
    SQLServer = "(LocalDb)\MSSQLLocalDB"
    SQLDatabase = "Absence" 
    SQLUsername = "Absence"
    SQLPassword = "Absence123"
    SQLConnectionString = 'DRIVER={ODBC Driver 17 for SQL Server};SERVER='+SQLServer+';DATABASE='+SQLDatabase+';UID='+SQLUsername+';PWD='+ SQLPassword

    conn = pyodbc.connect(SQLConnectionString)
    cursor = conn.cursor()

    cursor.execute("SELECT * FROM Cameras")
    IPs = []
    classrooms = []
    for row in cursor.fetchall():
        IPs.append(row[0])
        classrooms.append(row[1])
    cursor.close()
    conn.close()

    index = 0
    while index < len(IPs):
        cameraIP = IPs[index]

        ######## Real implementation: ########
        # vs = cv2.VideoCapture(f"http://{IP}/video")

        ######## POC: ########
        vs = cv2.VideoCapture(0)

        # Allow the webcam to warm up.
        time.sleep(2)

        personsInRoom = []
        iteration = 0
        # Counts people 10 times
        while iteration < 10:
            iteration += 1
            success, img = vs.read()
            faces = face_recognition.face_locations(img)
            totalFaces = len(faces)
            personsInRoom.append(totalFaces)
            time.sleep(0.5)

        # Set personCounter to the number with most entries / votes
        personCounter = max(set(personsInRoom), key=personsInRoom.count)
        previousPersonCounter = None

        if exists(f"Files/{cameraIP}.txt"):
            with open(f"Files/{cameraIP}.txt", "rb") as file:
                previousPersonCounter = file.readline()
        else:
            print("file did not exists")

        if  previousPersonCounter is None or personCounter != int(previousPersonCounter):
            with open(f'Files/{cameraIP}.txt', "w") as file:
                file.write(f"{personCounter}")

            conn = pyodbc.connect(SQLConnectionString)
            cursor = conn.cursor()
            cursor.execute(f"SELECT ClassroomNumber FROM Classrooms where ClassroomId = {classrooms[index]}")

            classroomNumber = cursor.fetchval()
            cursor.close()
            conn.close()

            print(classroomNumber)

            credentials = pika.PlainCredentials('python', 'python123')
            connection = pika.BlockingConnection(pika.ConnectionParameters(host='localhost', port=5672, virtual_host='/', credentials=credentials))
            channel = connection.channel()
            channel.queue_declare(queue="RoomUpdate", durable=True)
            channel.start_consuming()
            channel.basic_publish(exchange='', routing_key='RoomUpdate', body=f'{classroomNumber};{personCounter};{datetime.now().strftime("%d/%m/%Y %H:%M:%S")}')
            print(f"Succeded")
            connection.close()
        else:
            print(f"Operation was skipped! previousPersonCounter was the same value as the new personCounter")
        index += 1
except:
    with open('errors.txt', 'a') as file:
        file.write(f'[ERROR] - {datetime.now().strftime("%d/%m/%Y %H:%M:%S")} - {sys.exc_info()[0]}\n')