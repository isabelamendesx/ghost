using Ghost.Common.Services;
using Ghost.Infrastructure.Services;
using Library;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ghost.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection RegisterInfrastructureLayerServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddGeminiClient(configuration);
        services.AddSingleton<IGeminiService, GeminiService>();
        services.AddSingleton<IGitService, GitService>();

        return services;
    }
}
