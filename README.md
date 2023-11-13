# WebQWorker

This is a sample showing the [web-queue-worker](https://learn.microsoft.com/en-us/azure/architecture/guide/architecture-styles/web-queue-worker) pattern using .NET Aspire and Azure Service Bus.

The [_Web_](/Web/) project exposes a `/message` API that will publish to a service bus queue. You can use the [http file](/Web/Requests.http) to send messages to the queue.
The [_Worker_](/Worker/) project exposes a Blazor interactive server component to show messages being received from the service bus queue in real time.
The application requires a real Azure Service Bus instance (there are no emulators). 

The sample uses [Aspire.Hosting.Azure.Provisoning](#) to spin up a real Azure Service Bus instance in the specified subscription and location (see [below](#requirements) for more details).

## Requirements

The Azure subscription and location must be set in user secrets of the [_WebQWorker.AppHost_ project](/WebQWorker.AppHost/).
Navigate to that directory and run the following commands:

```
dotnet user-secrets set Azure:SubscriptionId <subscriptionId>
dotnet user-secrets set Azure:Location <location>
```

Replace \<subscriptionId\> and \<location\> with appropriate values. Locations values look like "westus" (see https://learn.microsoft.com/en-us/dotnet/api/azure.core.azurelocation?view=azure-dotnet for more details).

## Deployment

This example is can be deployed to Azure Container Apps using a [preview version of the Azure Developer CLI](#).