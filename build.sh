set -e

docker-compose down
docker-compose up -d --build
docker-compose run integration-tests app