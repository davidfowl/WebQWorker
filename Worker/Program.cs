using System.Threading.Channels;
using Worker;

var builder = WebApplication.CreateSlimBuilder(args);

builder.AddAzureServiceBus("bus");

builder.AddServiceDefaults();

builder.Services.AddRazorComponents();

builder.Services.AddHostedService<WorkerService>();

var channel = Channel.CreateUnbounded<string>();
builder.Services.AddSingleton(channel.Reader);
builder.Services.AddSingleton(channel.Writer);

var app = builder.Build();

app.UseStaticFiles();

app.MapDefaultEndpoints();

app.MapRazorComponents<App>().DisableAntiforgery();

app.Run();
