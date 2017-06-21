docker rm -f $(docker ps -aq)
docker-compose -f docker-compose.yml up --force-recreate --build