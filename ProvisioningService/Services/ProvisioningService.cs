using Grpc.Core;

namespace Provisioning.Services;

public class ProvisioningService : Provisioner.ProvisionerBase
{
    private readonly ILogger<ProvisioningService> logger; 
    public ProvisioningService (ILogger<ProvisioningService> logger) {
        this.logger = logger;
    }

    public override Task<ProvisioningResponse> ProvisiongModule(ProvisioningRequest request, ServerCallContext context)
    {
        logger.LogInformation("entered provision module endpoint");

        var response = new ProvisioningResponse();
        response.Status = "in progress";

        return Task.FromResult(response);
    }
}