using Ghost.Infrastructure.Models;
using Ghost.Infrastructure.Models.Enum;

namespace Ghost.Infrastructure.Repositories;

public interface IPromptRepository
{
    Prompt GetPromptByName(PromptType promptType);
}
