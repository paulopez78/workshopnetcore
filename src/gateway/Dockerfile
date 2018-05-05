FROM nginx:latest
ARG api
ARG static

COPY ${api}nginx.conf /etc/nginx/nginx.conf
COPY ${static} /var/www
