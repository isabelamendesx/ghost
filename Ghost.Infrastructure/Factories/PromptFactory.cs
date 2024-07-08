using Ghost.Infrastructure.Models;
using Ghost.Infrastructure.Models.Enum;
using Ghost.Infrastructure.Repositories;
using System.Text;

namespace Ghost.Infrastructure.Factories;

public class PromptFactory : IPromptFactory
{
    private readonly IPromptRepository _repository;

    public PromptFactory(IPromptRepository repository)
    {
        _repository = repository;
    }

    public string CreateCommitPrompt(PromptType promptType, string stagedChanges)
    {
        var template = _repository.GetPromptByName(promptType);
        return BuildPrompt(template, stagedChanges);
    }

    private string BuildPrompt(Prompt prompt, string stagedChanges)
    {
        var builder = new StringBuilder();

        var actions = new List<Action>
        {
            () => builder.AppendLine(prompt.Introduction.Replace("{stagedChanges}", stagedChanges)),
            () => AddFormat(builder, prompt.Format),
            () => AddSection(builder, "Types:", prompt.Types!),
            () => AddSection(builder, "Specification:", prompt.Specification),
            () => builder.AppendLine(prompt.Footer)
        };

        actions.ForEach(action => action());

        return builder.ToString();
    }

    private void AddFormat(StringBuilder builder, string format)
    {
        if (!string.IsNullOrWhiteSpace(format))
            builder.AppendLine(format);
    }

    private void AddSection(StringBuilder builder, string sectionTitle, List<string> sectionItems)
    {
        if (sectionItems != null && sectionItems.Any())
            builder.AppendLine(sectionTitle)
                   .AppendLine(string.Join("\n", sectionItems));
    }
}
