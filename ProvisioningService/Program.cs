using Microsoft.AspNetCore.Server.Kestrel.Core;
using Provisioning.Services;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    // Setup a HTTP/2 endpoint without TLS.
    options.ListenLocalhost(5222, o => o.Protocols =
        HttpProtocols.Http2);
});

builder.Services.AddGrpcReflection();
builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<ProvisioningService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

// Enable reflection in Debug mode.
if (app.Environment.IsDevelopment())
{
  app.MapGrpcReflectionService();
}

app.Run();
