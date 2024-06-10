using Ghost.Common.Enum;
using Ghost.Common.Models;
using Microsoft.Extensions.Configuration;

namespace Ghost.Infrastructure.Persistence.Repositories;

public class PromptRepository
{
    private readonly IConfiguration _configuration;

    public PromptRepository(IConfiguration configuration)
    {
        this._configuration = configuration;
    }

    public Prompt GetPromptByName(string templateName)
    {
        return _configuration.GetSection($"{nameof(PromptType)}:{templateName}").Get<Prompt>() ??
            throw new InvalidOperationException($"The template for {templateName} is not configured in appsettings.json.");
    }
}
