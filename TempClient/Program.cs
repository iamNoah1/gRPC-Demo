using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using Server;

using var channel = GrpcChannel.ForAddress("http://localhost:5226");
var client = new TemperatureReceiver.TemperatureReceiverClient(channel);

using var streamingCall = client.GetTemperatureStream(new Empty());

try
{
    await foreach (var weatherData in streamingCall.ResponseStream.ReadAllAsync())
    {
        Console.WriteLine($"{weatherData.DateTimeStamp.ToDateTime():s} | {weatherData.TemperatureC} C");
    }
}
catch (RpcException ex) when (ex.StatusCode == StatusCode.Cancelled)
{
    Console.WriteLine("Stream cancelled.");
}