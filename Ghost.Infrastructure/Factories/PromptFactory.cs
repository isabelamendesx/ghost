using Ghost.Infrastructure.Factories;
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

    public string CreateConventionalCommitPrompt(string stagedChanges)
    {
        var template = _repository.GetPromptByName(PromptType.Conventional);
        return BuildPrompt(template, stagedChanges);
    }

    public string CreateCommitStartingWithCodePrompt(string stagedChanges, string code)
    {
        var template = _repository.GetPromptByName(PromptType.StartingWithCode);
        return BuildPrompt(template, stagedChanges, code);
    }

    public string CreateCustomCommitPrompt(string stagedChanges)
    {
        var template = _repository.GetPromptByName(PromptType.Custom);
        return BuildPrompt(template, stagedChanges);
    }

    private string BuildPrompt(Prompt prompt, string stagedChanges, string? code = null)
    {
        var builder = new StringBuilder();

        var actions = new List<Action>
        {
            () => builder.AppendLine(prompt.Introduction.Replace("{stagedChanges}", stagedChanges)),
            () => AddFormat(builder, prompt.Format, code),
            () => AddSection(builder, "Types:", prompt.Types!),
            () => AddSection(builder, "Specification:", prompt.Specification),
            () => builder.AppendLine(prompt.Footer)
        };

        actions.ForEach(action => action());

        return builder.ToString();
    }

    private void AddFormat(StringBuilder builder, string format, string? code)
    {
        if (!string.IsNullOrWhiteSpace(format))
            builder.AppendLine(!string.IsNullOrWhiteSpace(code) ? format.Replace("{code}", code) : format);
    }

    private void AddSection(StringBuilder builder, string sectionTitle, List<string> sectionItems)
    {
        if (sectionItems != null && sectionItems.Any())
            builder.AppendLine(sectionTitle)
                   .AppendLine(string.Join("\n", sectionItems));
    }
}
