REM This will start the RabbitMQ Server, used in the project.

Powershell.exe docker build -t rmq-mqtt ../Compose/
docker run -it --rm -p 15672:15672 -p 5672:5672 -p 1883:1883 rmq-mqtt