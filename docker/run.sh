set -e
docker-compose down

docker-compose \
    -f docker-compose.dotnet-sdk.yml \
    -f docker-compose.dotnet-test.yml \
    up \
    --force-recreate

docker-compose \
    -f docker-compose.dotnet-sdk.yml \
    -f docker-compose.dotnet-publish.yml \
    up \
    --force-recreate

docker-compose \
    -f docker-compose.dotnet-runtime.yml \
    up \
    --force-recreate