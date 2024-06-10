using Library;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ghost.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection RegisterInfrastructureLayerServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddGeminiClient(configuration);
        return services;
    }

}
