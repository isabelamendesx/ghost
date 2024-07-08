using Ghost.Application.UseCases.ReviewMessage;
using MediatR;
using System.Text;

namespace Ghost.Application.UseCases.PickMessage;

public class PickMessageHandler : IRequestHandler<PickMessageCommand, bool>
{
    private readonly IMediator _mediator;
    private static int currentSelectionIndex = 0;

    public PickMessageHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<bool> Handle(PickMessageCommand request, CancellationToken cancellationToken)
    {
        var commitMessages = request.Messages;

        SetUpConsole();
        var selectedOption = DisplayMenu(commitMessages);
        return await _mediator.Send(new ReviewMessageCommand(commitMessages.ElementAt(selectedOption)));
    }

    private static void SetUpConsole()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.CursorVisible = false;
    }

    private static void DisplayCommitMessages(List<string> commitMessages)
    {
        Console.WriteLine("👻 Pick up a commit message:");

        for (int i = 0; i < commitMessages.Count; i++)
        {
            Console.ResetColor();
;
            if (i == currentSelectionIndex)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"> {commitMessages[i]}");
                continue;
            }

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"  {commitMessages[i]}");
        }
    }

    private static int DisplayMenu(List<string> commitMessages)
    {
        ConsoleKeyInfo key;
        do
        {
            Console.Clear();
            DisplayCommitMessages(commitMessages);

            key = Console.ReadKey(intercept: true);

            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    currentSelectionIndex = Math.Max(0, currentSelectionIndex - 1);
                    break;

                case ConsoleKey.DownArrow:
                    currentSelectionIndex = Math.Min(commitMessages.Count() - 1, currentSelectionIndex + 1);
                    break;
            }
        } while (key.Key != ConsoleKey.Enter);

        return currentSelectionIndex;
    }
}