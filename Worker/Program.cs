using WorkerService1;

var builder = WebApplication.CreateSlimBuilder(args);

builder.AddAzureServiceBus("bus");

builder.AddServiceDefaults();
builder.Services.AddHostedService<Worker>();

var app = builder.Build();

app.MapDefaultEndpoints();

app.Run();
