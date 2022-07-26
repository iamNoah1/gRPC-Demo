using Server.Services;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpcReflection();
builder.Services.AddGrpc();

// We need to set this up on mac os only I guess. Because http2 is not really supported. 
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5226, o => o.Protocols =
        HttpProtocols.Http2);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

// Enable reflection in Debug mode.
if (app.Environment.IsDevelopment())
{
  app.MapGrpcReflectionService();
}

app.Run();
