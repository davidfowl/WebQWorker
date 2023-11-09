using System.Threading.Channels;
using Azure.Messaging.ServiceBus;

public class WorkerService(
    ILogger<WorkerService> logger, 
    IConfiguration configuration, 
    ServiceBusClient client, 
    ChannelWriter<string> messageWriter) : BackgroundService
{
    private ServiceBusProcessor? _processor;
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Get the queue name from the config
        var queueName = configuration["queueName"];

        _processor = client.CreateProcessor(queueName);

        _processor.ProcessErrorAsync += (args) =>
        {
            logger.LogError(args.Exception, "Error processing message");

            return Task.CompletedTask;
        };

        _processor.ProcessMessageAsync += async (args) =>
        {
            logger.LogInformation("Received message: {Message}", args.Message.Body);

            await messageWriter.WriteAsync(args.Message.Body.ToString());
        };

        await _processor.StartProcessingAsync(stoppingToken);
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        return _processor?.StopProcessingAsync(cancellationToken) ?? Task.CompletedTask;
    }
}
