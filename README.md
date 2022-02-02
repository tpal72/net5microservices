# net5microservices
net5microservices

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

docker-compose -f docker-compose.yml -f docker-compose.override.yml down

# Docker cleanup

docker ps -aq
docker images -q

docker stop $(docker ps -aq)
docker rm $(docker ps -aq)
docker rmi $(docker images -q)
docker system prune





