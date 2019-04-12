FROM ubuntu
RUN apt-get update && apt-get install -yq \
    curl \
    dnsutils \
    && apt-get clean && \
    rm -rf /var/lib/apt/lists

ENTRYPOINT ["tail", "-f", "/dev/null"]
