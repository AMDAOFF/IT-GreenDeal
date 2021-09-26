REM Stop all running containers.

Powershell.exe docker container stop $(docker container ls -aq)