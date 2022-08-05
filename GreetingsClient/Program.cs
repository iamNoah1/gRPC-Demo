using Grpc.Net.Client;
using Server;

using var channel = GrpcChannel.ForAddress("http://localhost:5226");
var client = new Greeter.GreeterClient(channel);

using var streamingCall = client.ClientStreaming();

for (var i = 0; i < 3; i++) {
    await Task.Delay(500); // Gotta look busy

    Console.WriteLine("Sending greeting request through stream");
    await streamingCall.RequestStream.WriteAsync(new HelloRequest(){
        Name = "Noah " + i.ToString()
    });
}

await streamingCall.RequestStream.CompleteAsync();

var response = await streamingCall;
Console.WriteLine($"response: {response.Messages}");