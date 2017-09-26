
# BUILD STAGE
FROM microsoft/aspnetcore-build AS builder

COPY src/VotingApp.Domain /src/VotingApp.Domain

WORKDIR /src/VotingApp.Api
COPY src/VotingApp.Api/VotingApp.Api.csproj .
RUN dotnet restore

COPY src/VotingApp.Api .
RUN dotnet build  -o /build/ -c Release

# UNIT TEST STAGE
FROM builder AS unit-tests

WORKDIR /test/VotingApp.Tests
COPY test/VotingApp.Tests/VotingApp.Tests.csproj .
RUN dotnet restore

COPY test/VotingApp.Tests .
RUN dotnet test


# PUBLISH STAGE
FROM builder AS publish
WORKDIR /src/VotingApp.Api
RUN dotnet publish  -o /build/ -c Release


# PRODUCTION IMAGE STAGE
FROM microsoft/aspnetcore
WORKDIR /app
COPY --from=publish build/ .
ENTRYPOINT ["dotnet", "VotingApp.Api.dll"]