FROM alpine
ARG tests

WORKDIR /tests
RUN apk add jq curl --no-cache
COPY ${tests}test-api.sh .

ENTRYPOINT [ "./test-api.sh" ]