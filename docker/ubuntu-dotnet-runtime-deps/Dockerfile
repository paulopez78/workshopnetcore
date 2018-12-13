FROM ubuntu

RUN apt-get update \
    && apt-get install -y --no-install-recommends \
    ca-certificates \
    \
    # .NET Core dependencies
    libunwind8 \
    libunwind8-dev \
    gettext \
    libicu-dev \
    liblttng-ust-dev \
    libcurl4-openssl-dev \
    libssl-dev \
    uuid-dev \
    unzip \
    && rm -rf /var/lib/apt/lists/*