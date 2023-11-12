# WebQWorker

This is a sample showing the [web-queue-worker](https://learn.microsoft.com/en-us/azure/architecture/guide/architecture-styles/web-queue-worker) pattern using .NET Aspire and Azure Service Bus.

The _Web_ project exposes a `/message` API that will publish to a service bus queue.
The _Worker_ project exposes a Blazor UI to show messages being received from the service bus queue.
The application requires a real Azure Service Bus instance (since there are no emulators). 

The sample uses [Aspire.Hosting.Azure.Provisoning](#) to spin up a real Azure Service Bus instance in the specified subscription and location.

## Requirements

The azure subscription and location must be set in user secrets of the `WebQWorker.AppHost` project.
Navigate to that directory and run the following commands:

```
dotnet user-secrets set Azure:SubscriptionId <subscriptionId>
dotnet user-secrets set Azure:Location <location>
```

Replace \<subscriptionId\> and \<location\> with appropriate values. Locations values look like "westus" (see https://learn.microsoft.com/en-us/dotnet/api/azure.core.azurelocation?view=azure-dotnet for more details).
