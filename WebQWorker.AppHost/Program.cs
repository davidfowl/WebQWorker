var builder = DistributedApplication.CreateBuilder(args);

builder.AddAzureProvisioning();

var queueName = "myqueue";

var bus = builder.AddAzureServiceBus("bus", queueNames: [queueName]);

builder.AddProject<Projects.Web>("web")
    .WithEnvironment("queueName", queueName)
    .UseAzureTracing()
    .WithReference(bus);

builder.AddProject<Projects.Worker>("worker")
    .WithEnvironment("queueName", queueName)
    .UseAzureTracing()
    .WithReference(bus);

builder.Build().Run();
