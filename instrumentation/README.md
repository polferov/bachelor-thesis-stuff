To install run

```shell
helm install instrumentation --namespace instrumentation --create-namespace .
```

To instrument an app:

```yaml
apiVersion: apps/v1
kind: Deployment
spec:
  template:
    metadata:
      annotations:
        instrumentation.opentelemetry.io/inject-java: "instrumentation/my-instrumentation"
```

This will work, even when the `instrumentation` namespace is not the same as the app's namespace.