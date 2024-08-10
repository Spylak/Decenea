docker Commands

```shell
docker stop my-rabbitmq
docker remove my-rabbitmq

docker stop decenea
docker remove decenea
```~~~~

```shell
docker run -d --name my-rabbitmq --restart unless-stopped -e RABBITMQ_DEFAULT_USER=testUser -e RABBITMQ_DEFAULT_PASS=test12345A! -p 15672:15672 -p 5672:5672 rabbitmq:3-management
```

```shell
docker build -t decenea:latest .
```

```shell
docker run -d --network="host" --name decenea -p 5107:5107 microservices-orders-api:latest
```
