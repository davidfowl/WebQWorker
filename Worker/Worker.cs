using Azure.Messaging.ServiceBus;

namespace WorkerService1;

public class Worker(ILogger<Worker> logger, IConfiguration configuration, ServiceBusClient client) : BackgroundService
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

        _processor.ProcessMessageAsync += (args) =>
        {
            logger.LogInformation("Received message: {Message}", args.Message.Body);

            return Task.CompletedTask;
        };

        await _processor.StartProcessingAsync(stoppingToken);
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        return _processor?.StopProcessingAsync(cancellationToken) ?? Task.CompletedTask;
    }
}
