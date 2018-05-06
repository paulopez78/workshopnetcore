set -e

docker-compose \
    -f docker-compose.infra.yml \
    -f docker-compose.override.yml \
    -f docker-compose.yml \
    down

docker-compose \
    -f docker-compose.infra.yml \
    -f docker-compose.override.yml \
    -f docker-compose.yml \
    up --build -d 
    
docker-compose \
    -f docker-compose.infra.yml \
    -f docker-compose.override.yml \
    -f docker-compose.yml \
    run integration-tests commands