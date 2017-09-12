docker-compose build \
&& docker-compose push \
&& docker stack deploy -c ./docker/swarm/docker-compose.swarm.yml stack