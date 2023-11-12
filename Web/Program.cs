var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddAzureServiceBus("bus");
builder.Services.AddSingleton<MessageSender>();

var app = builder.Build();

app.MapDefaultEndpoints();

var queueName = builder.Configuration["queueName"];

app.MapPost("/message", async (MessageSender sender, Stream body) =>
{
    var payload = await BinaryData.FromStreamAsync(body);

    await sender.SendMessageAsync(payload);

    return Results.Accepted();
});

app.Run();
