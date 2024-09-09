# Call Me Maybe

Call Me Maybe is a simple Spring Boot application that calls another instance of itself recursively.

# Build Docker Image

```shell
./gradlew bootBuildImage --imageName polferov/call-me-maybe
```

If the image is needed for kubernetes, it must then be pushed to a registry.

```shell
docker push polferov/call-me-maybe
```

# Running the Application via helm

see helm chart [here](../call-me-maybe-helm/Chart.yaml)

# Running Locally via Docker Compose

The `docker-compose.yml` in this directory is plug and play.
A zipkin instnace will be made available at `http://localhost:9411` and the endpoint of one instance of the application
will be available at `http://localhost:9999`.

The application can be called via curl:

```shell
curl localhost:9999/call/100
```

This request will result in a `service ping-pong` between the two instances of the application.

Here, `100` can be replaced with any number >= 0.
It defines the request depth of the call chain.

## Verifying that the non-functional distributed tracing is due to inspectIt ocelot, and not my changes to ExplorViz

Open zipkin and see that all spans are not nested, as they should be, since the application is calling itself
recursively.