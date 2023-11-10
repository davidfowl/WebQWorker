using System.Threading.Channels;
using Worker;

var builder = WebApplication.CreateSlimBuilder(args);

builder.AddAzureServiceBus("bus");

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
