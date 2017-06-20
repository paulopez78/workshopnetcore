FROM microsoft/dotnet AS build-image

COPY tools tools

# copy shared libraries
COPY src/VotingApp /src/VotingApp

# restore packages creating caching layer 
WORKDIR /test/VotingApp.Tests
COPY test/VotingApp.Tests/VotingApp.Tests.csproj .
RUN dotnet restore

# run units
COPY test/VotingApp.Tests .
RUN dotnet test

# restore packages creating caching layer 
WORKDIR /src/VotingApp.Client
COPY src/VotingApp.Client/VotingApp.Client.csproj .
RUN dotnet restore

# publish release
COPY src/VotingApp.Client .
RUN dotnet publish  -o /build/ -c Release

# build runtime image from published release
FROM microsoft/dotnet:runtime
WORKDIR /app
COPY --from=build-image build/ .
ENTRYPOINT ["dotnet", "VotingApp.Client.dll"]