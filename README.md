# net5microservices
net5microservices

## Setup launchsettings

- Change Profile and Port number as per requirement


## Setup Mongo

### Pull mongo image

docker pull mongo
docker run -d -p 27017:27017 --name shopping-mongo mongo
docker logs -f shopping-mongo
docker exec -it shopping-mongo /bin/bash

docker start <docker ref> # if already running

### mongo cli commands

- mongo
- show databases
- use CatalogDB #Create new DB
- db.createCollection('Products')

docker run -d -p 27017:27017 --name catalogdb mongo
docker exec -it catalogdb /bin/bash


### Mongo GUI

Use mongoclient image - GUI for mongo

## Catalog Service - Setup Datalayer

- add DB details to appSettings.json
- Add ICatalogContext
- Implement above interface

## Containerize Application with Mongo using docker-compose

- Visual Studio -> Project -> Right Click -> Add -> Docker Orchestration Support -> Docker Compose
	> Automatically runs

- Add mongoDB to docker-compose file

docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d

- Rebuild  if change in any existing code

docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d --build

docker-compose -f docker-compose.yml -f docker-compose.override.yml down

## API with REDIS Cache Docker database

- REDIS -> Remote Dictionary Server
- Open Source NoSQL Database

docker pull redis

docker run -d -p 6379:6379 --name aspnetrun-redis redis

docker exec -it aspnetrun-redis /bin/bash

## REDIS-CLI commands

redis-cli

set key1 value1

get key1

- Add redis to docker-compose file. Add alpine version [light weight]

## Discount API PostgreSQL database

- docker pull postgres

- Admin Portal for postgres management using pgAdmin
- Connect to postgres db using pgadmin
- create table using pgadmin portal

- pgAdmin -> Add sever -> Name: DiscountServer, Host: discountdb, Post: 5432, cred: admin/admin1234

- NUGET: npgsql, Dapper (ORM)

- Migrate database on application startup - create DB/Add table/Add seeding data
  Modify Program.cs 

> gRPC Communication

- with PostGreSQL
- gRPS require HTTP/2 protocol, high performance, synchronous communication
- guthub -> aspnetrun -> run-aspnet-grpc
- best for communication between backend and internal APIs
- Add Asp.NET Core gRPc project to solution
- dotnet new grpc -o Discount.Grpc
- dotnet build
- dotnet run

- Add AutoMapper.Extensions.Microsft.DependancyInjection

### gRPC Consumption

- Consume Discount.gRPC in BAsket.API
- Add Connected Service (gRPC)
	- Add proto file
	- Create Client class

# Ordering API

### Intro
- SQL Server, REST Api, Entity Framework core code first
- Clean Architecture
- DDD [Domain Driver Architecture], CQRS [seperate queries and commands], SOLID
- MediatR, FluentValidation, Automapper

- Queries & ViewModels [reads], Commands & Domain-Model [Updates]

### SOLID Design principle

- S : Single Responsibility principle
- O : Open [for extensibility] - Close [for modification] principle
- L : Liskov substituion principle ???
- I : Interface segregation principle
- D : Dependancy Inversion principle

### Design principle - DIP - Dependancy Inversion principle

### Design principle - SoC - Seperation of concerns
- Low coupling, high cohesion

### Design principle - DDD - Domain Driven Design

### Clean architetcure with DDD

### CQRS - Command Query Request seperation
- Read and Write DB are different, usually
- Different READ and WRITE models

### Eventual consistency with CQRS pattern
- system will be eventual consistent
- Asynchronus processes
- No transactional dependancy

### Event sourcing
- Accumulating events in system
- Generate state from events

### CQRS Architecture

- Layers : Domain, Application, API, Infrastructure
- Domain layer: No dependancy
- Application layer: Depend on Domain, All Business use cases
	Contracts - Interfaces and Abstractions
	Features - Implement CQRS pattern and business concerns, use cases
	Behaviours - Validation, logging and other Cross cutting concerns
- App and Domain are CORE, no external dependancy
- Infrastructure on Application layer
- API on Application and Infrastructure layers
- ValueObject : Concept of Domain Driven Design

- Jason Taylor, Gill Cleeren for Clean Architecture



## Ordering.API Project

- ASP.NET Core API
- Add Clean Architecture layers
- Mediator Pattern - impelemented by MediatR nuget
- Command, CommandHandler, Query, QueryHandler, CommandValidator [Use Nuget FluentValidation]

- IPipeLineBehaviours - For X-Cutting concerns

- Create Registration extension class in Application layer to be used for DI in Startup.cs
- Add Automaper.Microsft.Extensions.DependancyInjection
- Add FluentValidation.Microsft.Extensions.DependancyInjection

## Infrastructure layer

> DB Layer
- DBContext from EntityFrameworkCore
- Code first approach
- IF EFCOre is replaced then only change the OrderContext object

> EMAIL Layer
- Use Sendgrid library [100 mail/day free]
- c# - Get email settings from application setting so use IOptions

> EF Core migrations for Code first approach
- Install-Package Microsoft.EntityFrameworkCore.Tools in Ordering.API
- Add-Migration InitialCreate
- dotnet tool install --global dotnet-ef
- Add "dotnet add package Microsoft.EntityFrameworkCore.Design --version 5.0.14" to project
- dotnet ef --startup-project ../Ordering.API/ migrations add InitialCreate
- Run Migrartion operaration on application startup automatically
- 

## SQL Server

sa
SwN12345678

# Async Communication with RabbitMQ and Mass Transit

- Between Basket and Ordering API

> Communication types
- Request Response
- Event Driven
- Hybrid

> RabbitMQ
- Message Queue System, other examples Apache Kafka, Az Svc Bus, etc
- Producer => RMQ [Exchange (Direct, Topic, Fanout, Header) - Bindings (link bet. exh and Qs) - Queues] => Consumer, FIFO
- Exchange types control routing to Queues
1. Direct -> Queue(s) with matching binding key -> One consumer
2. Topic -> Msg r sent to diff Q according to different Q(s) depending on content (based on routing algo, wildcard), -> 1 or more consumers
3. Fanout -> More that one Q (Broadcasting)
4. Header -> routes messages based on arguments containing headers and optional values

> Ports
- 5672 - RabbitMQ
- 15672 - Dashboard

> Mass Transit - Message sender and receiver, help route messages over RabbitMQ, others

### Add class library EventBus.Messages

### Produce RabbitMQ event from Basket.API

- Add EventBus.Messages project reference to Basket.API
- Modify Dockerfile to add Project
- Add Nuget MassTransit, MassTransit.RabbitMQ, MassTransit.AspNetCore
- Register in startup class

### Consume RabbitMQ event in Ordering.API

- Add EventBus.Messages project reference
- Modify Dockerfile to add Project
- Add Nuget MassTransit, MassTransit.RabbitMQ, MassTransit.AspNetCore
- Add Consumer side of code to Startup [Receiver endpoint]

## Containerize Ordering API

# API Gateway with OCELAOT

## <neela>API Gateway pattern</neela>

- Sigle entry point to multiple services
- BFF - backend for frontend pattern
- routing [reverse proxy], authentication, authorization, load balancing, throttling, logging, tracing
- request aggregation headers/query string transformation, correlation pass thru, 
- service discovery with eureka and consul
- cross cutting concerns or gateway offloading
- Multiple gateways should be used [for multiple client types]

### <hara>Ocelot</hara>

> Features

- lightweight, .net core based, opensource
- works with .net core only
- add empty .net core project

> Authentication and Authorization

- Use Identity microservices
- [aspnetrun github](https://github.com/aspnetrun)

> Application

- Ocelot Nuget Package
- Add Asp.Net core empty project
- Configure for logging in program.cs
- Add ocelot.json, ocelot.Development.json, ocelot.Local.json
- Port: 5010, Environmet: Local
- Add ocelot.json to program.cs
- Rate limiting in Ocelot Gateway
- Response caching	- install package Ocelot.Cache.CacheManager

> Dockerize

- Change ocelot.?.json - all urls should have container name in the URLs
- Host => container name and port number should be 80 [default one]
- Add Dockerfile, amend docker-compose and docker-compose.override files

### <hara>Request [Gateway] Aggregator pattern</hara>

- Target multiple microservices rquests into single http request
- use IHttpclientfactory - Use typed client, build retry and circuit breaker policies
- single request, multiple requests to backend systems, single response 

# Securing microservices with IdentityServer4 and Ocelot

- Cross cutting security concern
- Authentication as a service
- Protect API with OAuth2.0, MVC Client app with OpenId connect
- Backing with OCELOT API Gateway

# Shopping Web Application microservice

- Get WebApp from [aspnetrun-basic github](https://github.com/aspnetrun/run-aspnetcore-basics)
- Remove Entities/Repositories/Migrations/Data folders

# Container management with Portainer
- Deploy and manage containers
- Add to docker-compose file

admin
admin1234

---------------------------------------------------------------
# Microservices Observability, Resilience, Monitoring on .Net

# Implement CROSS Cutting concerns Elastic, Logsearch, Kibana, Web Status etc

## <neela>Elastic search</neela>

- For distributed logging - search and analytics engine
- RESRful API, fast, open source, scalable

## <neela>Logstash, Elastic Search, Kibana</neela>

- Collect & Transform => Search & Analyse => Visualize & Manage [Logstash => ElasticSearch => Kibana]
- Serilog => ASP.NET Logging library, Sink to ElasticSearch

> ASP.NET Logging is built in

- providers - console, eventsource, etc
- insert ILogger for logging
- Default logging to console
- 6 LogLevels - Trace = 0, Debug = 1, Information = 2, Warning = 3, Error = 4, Critical = 5, and None = 6.
- Log filtering -> appSettings.json add the namespace and logging level

> Configure

- use link [the carlo](https://github.com/thecarlo/elastic-kibana-netcore-serilog)
- docker-compose -> no need to specify network, default will be used


---------------------------------------------------------------


# Docker cleanup

docker ps -aq
docker images -q

docker stop $(docker ps -aq)
docker rm $(docker ps -aq)
docker rmi $(docker images -q)
docker system prune

docker rmi $(docker images -f "dangling=true" -q)

- image sizes of dangling and non-dangling images here:

docker system df -v

docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d
- Rebuild  if change in any existing code
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d --build
docker-compose -f docker-compose.yml -f docker-compose.override.yml down

<style>
laal{
    color:red;
}
hara{
    color:green;
}
neela{
    color:blue;
}
</style>