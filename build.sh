set -e

docker-compose down

docker-compose \
    -f docker-compose.infra.yml \
    -f docker-compose.override.yml \
    -f docker-compose.yml \
    up --build -d --remove-orphans
    
docker-compose run integration-tests commands