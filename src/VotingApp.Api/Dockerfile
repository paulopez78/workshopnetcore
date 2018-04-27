# BUILDER STAGE
FROM microsoft/aspnetcore-build AS builder
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
RUN dotnet publish ${api} -o /publish -c Release

# BUILDER STAGE
FROM microsoft/aspnetcore
WORKDIR /publish
COPY --from=builder /publish .
ENTRYPOINT ["dotnet", "VotingApp.Api.dll"]