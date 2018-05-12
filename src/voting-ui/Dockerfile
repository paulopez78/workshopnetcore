FROM node:alpine
ARG static

RUN npm install -g http-server 

WORKDIR /app
COPY ${static} .

ENTRYPOINT [ "http-server", "-p", "80" ]