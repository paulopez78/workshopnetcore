FROM microsoft/aspnetcore-build AS build-image

COPY tools tools

# copy shared libraries
COPY src/VotingApp.Lib /src/VotingApp.Lib

# restore packages creating caching layer 
WORKDIR /test/VotingApp.Tests
COPY test/VotingApp.Tests/VotingApp.Tests.csproj .
RUN dotnet restore

# run units
COPY test/VotingApp.Tests .
RUN dotnet test

# restore packages creating caching layer 
WORKDIR /src/VotingApp.Api
COPY src/VotingApp.Api/VotingApp.Api.csproj .
RUN dotnet restore

# publish release
COPY src/VotingApp.Api .
RUN dotnet publish  -o /build/ -c Release

# build runtime image from published release
FROM microsoft/aspnetcore
WORKDIR /app
COPY --from=build-image build/ .
ENTRYPOINT ["dotnet", "VotingApp.Api.dll"]