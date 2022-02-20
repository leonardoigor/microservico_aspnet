@echo off

 
cmd /k docker compose -f docker-compose.yaml -p dotnet_ms down --volumes --rmi all

exit