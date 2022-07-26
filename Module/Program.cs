using Module.Services;
using Grpc.Net.Client;
using Provisioning;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    // Setup a HTTP/2 endpoint without TLS.
    options.ListenLocalhost(5168, o => o.Protocols =
        HttpProtocols.Http2);
});

builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GreeterService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();

// The port number must match the port of the gRPC server.
using var channel = GrpcChannel.ForAddress("http://localhost:5222");
var client = new Provisioner.ProvisionerClient(channel);

var finishedReq = new ProvisioningFinishedRequest();
finishedReq.ModuleUrl = "url";

var reply = await client.ModuleReadyAsync(finishedReq);

Console.WriteLine("Press any key to exit..."); 
Console.ReadKey();
