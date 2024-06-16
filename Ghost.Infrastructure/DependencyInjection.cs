using Ghost.Common.Services;
using Ghost.Infrastructure.Factories;
using Ghost.Infrastructure.Persistence.Repositories;
using Ghost.Infrastructure.Repositories;
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
        services.AddSingleton<IPromptFactory, PromptFactory>();
        services.AddSingleton<IPromptRepository, PromptRepository>();

        return services;
    }
}
