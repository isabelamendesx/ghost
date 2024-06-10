using Cocona;
using Ghost.Common.Services;
using Ghost.Infrastructure.Models.Enum;
using Ghost.Infrastructure.Services;

namespace Ghost.Application.Commands;

public sealed class CommitCommand
{
    private readonly IGeminiService _geminiService;
    private readonly IGitService _gitService;

    public CommitCommand(IGitService gitService, IGeminiService geminiService)
    {
        _gitService = gitService;
        _geminiService = geminiService;
    }

    [Command("commit", Description = "Handles the commit process with the staged changes.")]
    public async Task Handle(
            [Option(Description = "An optional code that, if provided, will be the start of your commit message.")] 
            string? code,
            [Option(Description = "A flag indicating whether the Ghost should use the custom prompt or not.")] 
            bool custom = false)
    {
       var stagedChanges = await _gitService.GetStagedChangesAsync();
       var promptType = FindPromptType(code, custom);

            await ProcessCommitMessage(promptType, stagedChanges, code);
    }

    private async Task ProcessCommitMessage(PromptType promptType, string stagedChanges, string? code)
    {
        while (true)
        {
            Console.Clear();

            var message = await _geminiService.GetCommitMessage(promptType, stagedChanges, code);

            Console.WriteLine(message);

            Console.Write("Approve commit message? (y/n/r to regenerate): ");
            var input = Console.ReadLine()?.Trim().ToLower();

            if (input == "y")
            {
                await _gitService.CommitChangesAsync(message);
                Console.WriteLine("Changes committed successfully.");
                break;
            }
            else if (input == "r")
            {
                continue;
            }
            else
            {
                Console.WriteLine("Commit aborted.");
                break;
            }
        }
    }

    private PromptType FindPromptType(string? code, bool custom)
    {
        if (code is not null) return PromptType.StartingWithCode;
        if (custom) return PromptType.Custom;
        return PromptType.Conventional;
    }

}
