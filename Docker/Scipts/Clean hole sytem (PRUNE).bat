REM This will clean stop, remove and then clean the holde system.
REM This can be dangerous, if you have other docker systems running at your host.

Powershell.exe docker container stop $(docker container ls -aq)
Powershell.exe docker container rm $(docker container ls -aq)
Powershell.exe docker system prune -af