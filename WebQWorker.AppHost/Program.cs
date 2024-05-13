var builder = DistributedApplication.CreateBuilder(args);

builder.AddAzureProvisioning();

var queueName = "myqueue";

var bus = builder.AddAzureServiceBus("bus").AddQueue(queueName);

builder.AddProject<Projects.Web>("web")
    .WithEnvironment("queueName", queueName)
    .UseAzureTracing()
    .WithReference(bus);

builder.AddProject<Projects.WorkerNoUI>("worker")
    .WithEnvironment("queueName", queueName)
    .UseAzureTracing()
    .WithReference(bus);

builder.Build().Run();
