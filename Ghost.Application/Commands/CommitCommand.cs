using Cocona;
using Figgle;
using Ghost.Application.Filters;
using Ghost.Application.UseCases.EditMessage;
using Ghost.Application.UseCases.PickMessage;
using Ghost.Application.UseCases.ReviewMessage;
using Ghost.Common.Services;
using Ghost.Infrastructure.Models.Enum;
using Ghost.Infrastructure.Services;
using MediatR;
using Microsoft.Extensions.Options;
using System.Text;

namespace Ghost.Infrastructure.Commands;

public sealed class CommitCommand
{
    private readonly IGeminiService _geminiService;
    private readonly IGitService _gitService;
    private readonly IMediator _mediator;

    public CommitCommand(IGitService gitService, IGeminiService geminiService, IMediator mediator)
    {
        _gitService = gitService;
        _geminiService = geminiService;
        _mediator = mediator;
    }

    [PreExecutionFilter]
    [Command("commit", Description = "Handles the commit process with the staged changes.")]
    public async Task Handle
        (
            [Option(Description = "An optional code that, if provided, will be the start of your commit message.")]
            string? code,
            [Option(Description = "A flag indicating whether the Ghost should use the custom prompt or not.")]
            bool custom = false
        )
    {
        var stagedChanges = await _gitService.GetStagedChangesAsync();
        var promptType = FindPromptType(code, custom);

        await ProcessCommitMessage(promptType, stagedChanges, code);
    }

    private async Task ProcessCommitMessage(PromptType promptType, string stagedChanges, string? code)
    {
        Console.WriteLine(FiggleFonts.Standard.Render("Ghost."));

        var messages = await _geminiService.GetCommitMessages(promptType, stagedChanges, code);

        var selectedMessage = await _mediator.Send(new PickMessageCommand { Messages = messages });

        var result = await _mediator.Send(new ReviewMessageCommand(selectedMessage));

        if(result) 
            Console.WriteLine("\n✅ Changes committed successfully!");
        else 
            Console.WriteLine("\n👻 Commit aborted");
    }

    private PromptType FindPromptType(string? code, bool custom)
    {
        if (code is not null) return PromptType.StartingWithCode;
        if (custom) return PromptType.Custom;
        return PromptType.Conventional;
    }
}
