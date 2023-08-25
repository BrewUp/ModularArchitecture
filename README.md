# BrewUp - Modular Architecture

## Scenario
Starting a new project using a microservice architecture requires a lot of extra effort.  
We know that a monolithic solution also has some restrictions, as scalability.  
What if we tell you that there is an alternative solution that merges the best of two scenarios?  

## What is Modular Architecture
The idea behind this architecture style is simple:  
> - **Low Coupling**: Each module should be independent of other modules in the system  
> - **High Cohesion**: Components of the module are all related thus making it easier to understand what module does as a self-contained subsystem (SRP)  

# The solution
From a high level, the architecture defines an API, two modules, and a common event bus for communication (`IMediatr`, based on [MediatR](https://github.com/jbogard/MediatR)).  
Each module is separated into some projects, which are implemented as separate binaries: 
> - `Facade` for the handling of all requests  
> - `Domain` for domain logic and as a handler for all Commands  
> - `Messages` for the definitions of Commands and Events classes  
> - `ReadModel` for the handlers of DomainEvents and the Read Model management  
> - `SharedKernel` that contains DomainId, DTOs, and all shared components

## Fitness Functions
This solution implements special tests that ensure that the architecture, defined at the beginning of the project, remains valid.  
In this solution we used two different libraries  
> - [ArchUnitNET](https://github.com/TNG/ArchUnitNET), a library for enforcing architecture constraints (eg. avoid importing classes from forbidden namespaces). It is the C# port of [ArchUnit](https://www.archunit.org/) Java library.  
> - [NetArchtest](https://github.com/BenMorris/NetArchTest), a library with similar intent but with idiomatic fluent API for .Net Standard.  

## Infrastructure
Steps to setup the infrastructure.  
The app runs on [.NET Core framework](https://dotnet.microsoft.com/) and [ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-7.0) framework.  
It uses [EventStoreDB](http://www.eventstore.com/) to save Domain Events and [MongoDB](https://www.mongodb.com/) for the Read Model(s).  

### Prerequisites
The .NET Core 7 SDK or later is required for some of the instructions in this document.  
Download the latest SDK from here: [Download .NET](https://dotnet.microsoft.com/en-us/download)

## Dealing with secrets
See [Safe storage of app secrets in development in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-7.0&tabs=windows).  

### Create a self signed certificate
ASP.NET Core uses HTTPS by default. HTTPS relies on certificates for trust, identity, and encryption.  
Before starting, we need to generate a certificate on the local machine.  

See: [Generate self-signed certificates with the .NET CLI](https://learn.microsoft.com/en-us/dotnet/core/additional-tools/self-signed-certificates-guide#create-a-self-signed-certificate) for more info.  


#### Remove the certificate if already present
We need to delete the certificate associated to the C# project of the .NET ASP.NET Core web API.  
In this repository, the project is `BrewUp\BrewUp.csproj`:  
~~~sh
dotnet user-secrets remove "Kestrel:Certificates:Development:Password" -p BrewUp\BrewUp.csproj
dotnet dev-certs https --clean
~~~

#### Windows
It is recommended that the name of the certificate is the same of the C# project for the web API>  
In this repository, it will be `BrewUp.pfx`.  
For the password we use `MySecretKey`.  
Don't change these values; if you have to, update `docker-compose.yml` accordingly.  
Generate the certificate using PowerShell or .NET CLI.  

With PowerShell:
~~~sh
dotnet dev-certs https -ep "$env:USERPROFILE\.aspnet\https\BrewUp.pfx" -p MySecretKey
dotnet dev-certs https --trust
~~~

With .NET CLI:  
~~~sh
dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\BrewUp.pfx -p MySecretKey
dotnet dev-certs https --trust
~~~

#### macOS or Linux
Generate the certificate using .NET CLI:  
~~~sh
dotnet dev-certs https -ep ${HOME}/.aspnet/https/BrewUp.pfx -p MySecretKey
dotnet dev-certs https --trust
~~~

See: [Starting a container with https support using docker compose](https://learn.microsoft.com/en-us/aspnet/core/security/docker-compose-https?view=aspnetcore-5.0)

#### Check the presence of the certificate

Windows:  
~~~powershell
dir "$env:USERPROFILE\.aspnet\https"
~~~

Linux and macOS:  
~~~sh
dir ${HOME}/.aspnet/https
~~~

## Build Docker image
Before starting up all the needed containers, we need to build the image for the app: 
~~~sh
docker build -t "brewup:latest" .
~~~
This command will look for (and build) the image described in [Dockerfile](Dockerfile)

## Run the app
To run the app, you can use `docker compose`.  
See V2 reference: [docker compose](https://docs.docker.com/compose/reference/#command-options-overview-and-help):  

~~~sh
docker compose up
~~~
It will look for (and run) the default file, [docker-compose.yml](docker-compose.yml).  

Tip: To run arbitrary `.yml` files, use `-f` option:
~~~sh
# docker compose -f <file>.yml, eg:
docker compose -f docker-compose-test.yml
~~~

## Access to the containers services
When the containers are up and running, we can access to them from our local machine.  

## BrewUp Web API Swagger
Access to the [Swagger UI](https://swagger.io/tools/swagger-ui/) at this URL: [https://localhost:5001/documentation](https://localhost:5001/documentation).  

## Access to Mongo with an external tool
With tools like [Robo3T](https://robomongo.org/), you can use this URI to establish a connection:
~~~sh
mongodb://root:password@brewup-mongodb:27017/
~~~
See: [Connection String URI Format](https://www.mongodb.com/docs/manual/reference/connection-string/).  

## Access to Mongo using Mongo Express
You can access to the [Mongo Express](https://github.com/mongo-express/mongo-express) web interface at this URL: [http://localhost:29017/](http://localhost:29017/).  

### Access to EventStore
You can access to `EvenStore Dashboard` at this URL: [http://localhost:2113/](http://localhost:2113/).  


## Shutdown the app
~~~sh
docker compose down
~~~

## FAQ
Some useful FAQs. 
### Starting a container with https support using docker compose
[Hosting ASP.NET Core images with Docker Compose over HTTPS](https://learn.microsoft.com/en-us/aspnet/core/security/docker-compose-https?view=aspnetcore-7.0)

### Exploring a Docker image content
Explore the filesystem of an image to be sure all is in place:  
~~~sh
docker run --rm -it --entrypoint=/bin/bash name-of-image
~~~

