using Azure.Messaging.ServiceBus;

/// <summary>
/// A class that can send messages to a Service Bus queue. It's a singleton that 
/// keeps a reference to the ServiceBusClient and ServiceBusSender for the specified queue.
/// See https://learn.microsoft.com/en-us/azure/service-bus-messaging/service-bus-performance-improvements?tabs=net-standard-sdk-2#reusing-factories-and-clients for
/// more information on reusing the client and sender.
/// </summary>
internal class MessageSender(ServiceBusClient client, IConfiguration config) : IAsyncDisposable
{
    private readonly ServiceBusSender _sender = client.CreateSender(config["queueName"] ?? throw new InvalidOperationException("queueName is required"));

    public Task SendMessageAsync(BinaryData payload) => 
        _sender.SendMessageAsync(new ServiceBusMessage(payload));

    public ValueTask DisposeAsync() =>
        _sender.DisposeAsync();
}