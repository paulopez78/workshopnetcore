#!/bin/bash
set -e
docker-compose build
docker-compose push
docker stack deploy -c docker-compose.swarm.yml stack
