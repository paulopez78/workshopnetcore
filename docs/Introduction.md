# Information

## Setup and installation:
* [.NET Core SDK](https://www.microsoft.com/net/download/core)
* [Visual Studio Code](https://code.visualstudio.com/) with C# extension 
* [Docker CE (edge version)](https://www.docker.com/community-edition#/download)


## dotnet CLI and SDK
* What is installed and where? Quick look at the dotnet core installation folder.
* [SDK architecture](https://docs.microsoft.com/en-us/dotnet/core/tools/cli-msbuild-architecture)
* Introduction to donet CLI basic commands and new SDK architecture based on msbuild targets 
    * `dotnet --info`
    * `dotnet --version`
    * `dotnet new`
    * `dotnet restore`
    * `dotnet run`

## New Project.csproj
* New restore and build system based on nuget and new msbuild 15
* [Packages, Metapackages and Frameworks](https://docs.microsoft.com/en-us/dotnet/core/packages)
* `dotnet restore` and the `project.assets.json`
* Expanding the csproj file with `dotnet msbuild /pp:fullproject.xm`
