# BUILDER STAGE
FROM microsoft/dotnet:sdk AS builder
ARG api
ARG tests
ARG domain

# RESTORE
COPY ${api}*.csproj ${api}
COPY ${domain}*.csproj ${domain}
COPY ${tests}*.csproj ${tests}

RUN dotnet restore ${api}
RUN dotnet restore ${tests}

# UNIT TESTS
COPY ${api} ${api}
COPY ${tests} ${tests}
COPY ${domain} ${domain}

RUN dotnet test ${tests}

# PUBLISH
RUN dotnet publish ${api} -o /publish -c Debug

# BUILDER STAGE
FROM microsoft/dotnet:2.2-aspnetcore-runtime-alpine
WORKDIR /publish
COPY --from=builder /publish .
ENTRYPOINT ["dotnet", "VotingApp.Queries.dll"]