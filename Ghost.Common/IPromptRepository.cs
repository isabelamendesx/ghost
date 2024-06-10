using Ghost.Common.Enum;
using Ghost.Common.Models;

namespace Ghost.Common;

public interface IPromptRepository
{
    Prompt GetPromptByName(PromptType promptType);
}
