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
* Run votingapp with net461,net451 and netcoreapp20
* Use netstandard1.0 and netstandard2.0 in the VotingDomain project
* Create leggacy project (logger and utils) and use it in the votingapp
* Move legacy to netstandard lib and show platform not supported exceptions.

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

### Building and Deploying the VotingApp with Docker Compose
*   dotnet publish with SDK images
*   [Multistage docker file for building runtime images](https://docs.docker.com/engine/userguide/eng-image/multistage-build/)
*   Debugging with docker, vscode and .net core. Docker support for Visual Studio.

### Upgrade to dotnetcore 2.1
*   Show new docker images layout
*   Run with the new dotnet alpine 2.1 images 

## Session 3: orchestrate with kubernetes

### Deploy to Minikube 
*   Install minikube
*   Addons (heapster)
*   kubectl basic commands: context, get
*   reuse the docker engine for building and pushing
*   run votingapp within a pod, use kubectl port-forward
*   expose a service for the votingapp pod
*   create yml definitions for deployment and services
*   enable ingress addon
*   scale the votingapp
*   full ci cd with docker and kubectl

### Refactoring towards EventSourcing, CQRS and microservices
*   [Sample voting app with eventsourcing architecture](https://github.com/paulopez78/workshopnetcore/tree/cqrs)
*   create votingapp commands and votingapp queries
*   Add rabbit and postgresql images 
*   Development workflow with docker

### Deploy with helm
*   Deploy infrastructure with helm charts: rabbit and postgresql