# Contents

## Session 1: dotnet core

### Get started with the dotnet CLI and the new SDK
* What is installed and where? Quick look at the dotnet core installation folder.
* dotnet.exe, SDK, coreclr, corefx 

### Hello World: Understanding the new Project.csproj
* Compare with previous csproj (nuget, assemblyinfo, properties)
* Expanding the csproj file with `dotnet msbuild /pp:fullproject.xml`
* msbuild structured log `dotnet msbuild /bl`
* SDK is not just for dotnetcore: Compile for different target frameworks

### Creating the VotingApp library using TDD
* Use VSCode integrated terminal for running dotnet cli commands.
* Create xunit project
* Install and understand how the C# extension work.
* Run and debug unit tests.
* Extend the dotnet cli tooling. dotnet watch
* Create classlib project.
* Autogenerate the sln file for better project management.

### Creating the ASP.NET Core VotingApp API
* Use the debugger and understand the `launch.json` file.
* New default builder and default configuration
* WebHost DefaultBuilder 
* Built-in Configuration, Dependency Injection and Logging.

### .NET Standard
* [netstandard](https://www.slideshare.net/PauLpez3/demystifying-net-standard-77852581)
* [Logger Sample](https://github.com/paulopez78/workshopnetcore/tree/netstandard/src/LegacyLoggingLib)

## Session 2: dotnetcore and docker

### Publishing the VotingApp API in different frameworks and runtimes
*   dotnet publish command
*   Portable vs Linux vs Windows

### Running the VotingApp with Docker
*   Running inside docker ubuntu container: runtime deps, ASPNETCORE_URLS, port mapping, ...
*   Automate it with a Dockerfile
*   Simplify with the dotnet runtime deps image
*   Better to use the portable publish with dotnet runtime image, docker layers and cache.
*   What is a docker container and a docker image, different dotnet runtime images.
*   Run with the new dotnet alpine 2.1 images 

### Building and Deploying the VotingApp with Docker Compose
*   dotnet publish with SDK images 
*   [Multistage docker file for building runtime images](https://docs.docker.com/engine/userguide/eng-image/multistage-build/)
*   Debugging with docker, vscode and .net core

## Session 3: orchestrate with kubernetes

### Refactoring towards EventSourcing, CQRS and microservices
*   [Sample voting app with eventsourcing architecture](https://github.com/paulopez78/workshopnetcore/tree/eventsourcing)
*   create votingapp commands and votingapp queries
*   Add rabbit and postgresql images 
*   Development workflow with docker

### Deploy to Minikube 
*   Install minikube
*   kubectl basic commands
*   Create deployment, pods, services, ingress...

### Deploy with helm
*   Deploy infrastructure with helm charts: rabbit and postgresql

