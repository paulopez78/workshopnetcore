# Docker Commands

* Remove all containers:
    ```bash
    docker rm -f $(docker ps -aq)
    ```
* Run interactive terminal using using image `microsoft/dotnet:sdk`
    * **Bash**
    ```bash
    docker run -it -v $(PWD):/app --workdir /app microsoft/dotnet:sdk
    ```
    * **Powershell**
    ```powershell
    docker run -it -v ${PWD}:/app --workdir /app microsoft/dotnet:sdk
    ```
* Run `VotingApp.Api` using image `microsoft/dotnet:sdk`
    * **Bash**
    ```bash
    docker run -p 5000:80 \
        -e ASPNETCORE_URLS=https://*:80 \
        -v $(PWD):/app \
        --workdir /app \
        --name votingapp \
        microsoft/dotnet:sdk \
        bash -c "dotnet restore && dotnet run -p VotingApp.Api/*.csproj"
    ```
    * **Powershell**
    ```powershell
    docker run -p 5000:80 `
        -e ASPNETCORE_URLS=https://*:80 `
        -v ${PWD}:/app `
        --workdir /app `
        --name votingapp `
        microsoft/dotnet:sdk `
        bash -c "dotnet restore && dotnet run -p VotingApp.Api/*.csproj"
    ```
* Publish `VotingApp.Api` using image `microsoft/aspnetcore-build`
    * **Bash**
    ```bash
    docker run \
        -v $(PWD):/app \
        --workdir /app \
        --name votingapp-build \
        microsoft/aspnetcore-build \
        bash -c "dotnet restore && dotnet publish VotingApp.Api/*.csproj -o build"
    ```
    * **Powershell**
    ```powershell
    docker run `
        -v ${PWD}:/app `
        --workdir /app `
        --name votingapp-build `
        microsoft/aspnetcore-build `
        bash -c "dotnet restore && dotnet publish VotingApp.Api/*.csproj -o build"
    ```