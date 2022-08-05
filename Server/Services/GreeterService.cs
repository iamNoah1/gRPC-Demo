using Grpc.Core;

namespace Server.Services;

public class GreeterService : Greeter.GreeterBase
{
    private readonly ILogger<GreeterService> logger;
    public GreeterService(ILogger<GreeterService> logger)
    {
        this.logger = logger;
    }

    public override Task<HelloReply> UnaryCall(HelloRequest request, ServerCallContext context)
    {
        logger.LogInformation("Received unary greeting request");
        return Task.FromResult(new HelloReply
        {
            Message = "Hello " + request.Name
        });
    }

    public override async Task<HelloListReply> ClientStreaming(IAsyncStreamReader<HelloRequest> requestStream, ServerCallContext context)
    {
        logger.LogInformation("Received greeting stream");
        HelloListReply reply = new HelloListReply();
        while (await requestStream.MoveNext())
        {
            logger.LogInformation("Received greeting request from greeting stream");
            reply.Messages.Add(new HelloReply(){
                Message = "Hi " + requestStream.Current.Name
            });
        }

        return reply;
    }
}
