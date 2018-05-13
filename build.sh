set -e

docker-compose \
    -f docker-compose.override.yml \
    -f docker-compose.yml \
    down

docker-compose \
    -f docker-compose.override.yml \
    -f docker-compose.yml \
    up -d --build

docker-compose \
    -f docker-compose.override.yml \
    -f docker-compose.yml \
    run integration-tests app