using Ghost.Infrastructure.Models;
using Ghost.Infrastructure.Models.Enum;
using Ghost.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;

namespace Ghost.Infrastructure.Persistence.Repositories;

public class PromptRepository : IPromptRepository
{
    private readonly IConfiguration _configuration;

    public PromptRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Prompt GetPromptByName(PromptType promptType)
    {
        return _configuration.GetSection($"{nameof(PromptType)}:{promptType}").Get<Prompt>() ??
            throw new InvalidOperationException($"The template for {nameof(promptType)} is not configured in appsettings.json.");
    }
}
