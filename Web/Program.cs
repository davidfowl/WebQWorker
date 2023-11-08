using Azure.Messaging.ServiceBus;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddAzureServiceBus("bus");

var app = builder.Build();

app.MapDefaultEndpoints();

var queueName = builder.Configuration["queueName"];

app.MapPost("/message", async (ServiceBusClient client, Stream body) =>
{
    var message = new ServiceBusMessage(await BinaryData.FromStreamAsync(body));

    await client.CreateSender(queueName).SendMessageAsync(message);

    return Results.Accepted();
});

app.Run();
