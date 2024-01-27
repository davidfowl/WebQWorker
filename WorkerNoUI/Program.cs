
using System.Threading.Channels;
using Worker;

var builder = Host.CreateApplicationBuilder(args);

builder.AddAzureServiceBus("bus");

builder.AddServiceDefaults();

builder.Services.AddHostedService<WorkerService>();
builder.Services.AddHostedService<MessageConsumer>();

builder.Services.AddSingleton<MessageListener>();

var host = builder.Build();

host.Run();

public class MessageConsumer(MessageListener messageListener, ILogger<MessageConsumer> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var channel = Channel.CreateUnbounded<string>();

        await messageListener.SubscribeAsync(channel);

        await foreach (var message in channel.Reader.ReadAllAsync(stoppingToken))
        {
            logger.Log(LogLevel.Information, 0, message, null, static (s, _) => s);
        }
    }
}