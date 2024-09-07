The services must be started in a specific order.
The config-server must be started first, followed by the discovery-server,
then all other services can be started in any order.

```shell
helm install petclinic-microservices-config . --values values.config.yaml 
```

```shell
helm install petclinic-microservices-discovery . --values values.discovery.yaml
```

```shell
helm install petclinic-microservices-services . --values values.others.yaml
```