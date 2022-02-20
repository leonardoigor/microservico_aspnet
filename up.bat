@echo off

cmd /k docker compose -f ./docker-compose.yaml -p dotnet_ms up --renew-anon-volumes


exit