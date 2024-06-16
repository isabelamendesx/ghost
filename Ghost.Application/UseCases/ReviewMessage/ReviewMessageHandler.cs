using Ghost.Application.UseCases.EditMessage;
using Ghost.Infrastructure.Services;
using MediatR;

namespace Ghost.Application.UseCases.ReviewMessage;

public class ReviewMessageHandler : IRequestHandler<ReviewMessageCommand, bool>
{
    private readonly IGitService _gitService;
    private readonly IMediator _mediator;

    public ReviewMessageHandler(IGitService gitService, IMediator mediator)
    {
        _gitService = gitService;
        _mediator = mediator;
    }

    public async Task<bool> Handle(ReviewMessageCommand request, CancellationToken cancellationToken)
    {
        string selectedMessage = request.message;

        while (true)
        {
            Console.WriteLine($"\n✅ You selected: {selectedMessage}");
            Console.Write("\n👻 Approve commit message? (y/n/e to edit): ");
            var input = Console.ReadLine()?.Trim().ToLower();

            if (input == "y")
            {
                await _gitService.CommitChangesAsync(selectedMessage);
                return true;
            }
            else if (input == "e")
            {
                selectedMessage = await _mediator.Send(new EditMessageCommand { Message = selectedMessage });
                continue;
            }
            else
            {
                return false;
            }
        }
    }
}
