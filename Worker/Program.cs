using WorkerService1;

var builder = Host.CreateApplicationBuilder(args);

builder.AddAzureServiceBus("bus");

builder.AddServiceDefaults();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
