import pymssql

server = "(localdb)\MSSQLLocalDB"
user = "Absence"
password = "Absence123"
database = "Absence"

conn = pymssql.connect(server, user, password, database)
cursor = conn.cursor()
cursor.execute('SELECT * FROM Cameras')
row = cursor.fetchmany()
while row:
    print("IP=%s, Name=%d" % (row[0], row[1]))

conn.close()
