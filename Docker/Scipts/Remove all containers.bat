REM Removes all stopped containers

Powershell.exe docker container rm $(docker container ls -aq)