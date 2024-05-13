using Worker;

var builder = WebApplication.CreateSlimBuilder(args);

builder.AddAzureServiceBusClient("bus");

builder.AddServiceDefaults();

builder.Services.AddRazorComponents()
        .AddInteractiveServerComponents();

builder.Services.AddHostedService<WorkerService>();

builder.Services.AddSingleton<MessageListener>();

var app = builder.Build();

app.UseStaticFiles();

app.MapDefaultEndpoints();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
