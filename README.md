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

Use mongoclient image

## Setup Entities

## Setup Datalayer

- add DB details to appSettings.json
- Add ICatalogContext
- Implement above interface

## Containerize Application with Mongo using docker-compose

- Visual Studio -> Project -> Right Click -> Add -> Docker Orchestration Support -> Docker Compose
	> Automatically runs

- Add mongoDB to docker-compose file

docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d

### Rebuild i if change in any existing code
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

### gRPC Communication

- Consume Discount.gRPC in BAsket.API
- Add Connected Service (gRPC)
	- Add proto file
	- Create Client class

# Container management with Portainer
- Deploy and manage containers
- Add to docker-compose file

admin
admin1234

# Docker cleanup

docker ps -aq
docker images -q

docker stop $(docker ps -aq)
docker rm $(docker ps -aq)
docker rmi $(docker images -q)
docker system prune

