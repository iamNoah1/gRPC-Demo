using Grpc.Core;
using Google.Protobuf.WellKnownTypes;

namespace Server.Services;

public class TemperatureService : TemperatureReceiver.TemperatureReceiverBase
{
    private readonly ILogger<TemperatureService> logger;
    public TemperatureService(ILogger<TemperatureService> logger)
    {
        this.logger = logger;
    }

    public override async Task GetTemperatureStream(Empty _, IServerStreamWriter<TemperatureData> responseStream, ServerCallContext context)
    {
        var rng = new Random();
        var now = DateTime.UtcNow;

        var i = 0;
        while (!context.CancellationToken.IsCancellationRequested && i < 20)
        {
            await Task.Delay(500); // Gotta look busy

            var forecast = new TemperatureData
            {
                DateTimeStamp = Timestamp.FromDateTime(now.AddDays(i++)),
                TemperatureC = rng.Next(-20, 55),
            };

            logger.LogInformation("Sending WeatherData response");

            await responseStream.WriteAsync(forecast);
        }
    }
}