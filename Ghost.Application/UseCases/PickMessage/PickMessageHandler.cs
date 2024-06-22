using Ghost.Application.UseCases.ReviewMessage;
using MediatR;
using System.Text;

namespace Ghost.Application.UseCases.PickMessage;

public class PickMessageHandler : IRequestHandler<PickMessageCommand, bool>
{
    private readonly IMediator _mediator;

    public PickMessageHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<bool> Handle(PickMessageCommand request, CancellationToken cancellationToken)
    {
        var messages = request.Messages;

        SetupConsole();
        DisplayInstructions();

        var selectedOption = HandleUserInput(messages);

        return await _mediator.Send(new ReviewMessageCommand(messages.ElementAt(selectedOption)));
    }

    private static void SetupConsole()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.CursorVisible = false;
    }

    private static void DisplayInstructions()
    {
        Console.WriteLine("👻 Pick up a commit message:");
    }

    private static int HandleUserInput(IEnumerable<string> messages)
    {
        (int left, int top) = Console.GetCursorPosition();
        int selectedOption = 0;
        const string decorator = "> ";
        bool isSelected = false;

        while (!isSelected)
        {
            Console.SetCursorPosition(left, top);
            ClearLines(messages.Count());
            DisplayMessages(messages, selectedOption, decorator);

            ConsoleKeyInfo key = Console.ReadKey(intercept: true);
            selectedOption = UpdateSelectedOption(key, selectedOption, messages.Count());
            isSelected = key.Key == ConsoleKey.Enter;
        }

        return selectedOption;
    }

    private static void ClearLines(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Console.WriteLine(new string(' ', Console.WindowWidth));
        }
        Console.SetCursorPosition(0, Console.GetCursorPosition().Top - count);
    }

    private static void DisplayMessages(IEnumerable<string> messages, int selectedOption, string decorator)
    {
        for (int i = 0; i < messages.Count(); i++)
        {
            var message = messages.ElementAt(i).Trim();
            var backgroundColor = BackgroundColors[i % BackgroundColors.Count];
            var coloredOption = $"{backgroundColor}[{i + 1}]\u001b[0m";
            var messageColor = selectedOption == i ? "\u001b[90m" : "";
            Console.WriteLine($"{(selectedOption == i ? decorator : "  ")}{coloredOption} {messageColor}{message}\u001b[0m");
        }
    }

    private static int UpdateSelectedOption(ConsoleKeyInfo key, int currentOption, int messageCount)
    {
        switch (key.Key)
        {
            case ConsoleKey.UpArrow:
                return currentOption == 0 ? messageCount - 1 : currentOption - 1;
            case ConsoleKey.DownArrow:
                return currentOption == messageCount - 1 ? 0 : currentOption + 1;
            default:
                return currentOption;
        }
    }

    private static readonly List<string> BackgroundColors = new List<string>
    {
        "\u001b[48;5;198m\u001b[97m",
        "\u001b[48;5;210m\u001b[97m",
        "\u001b[48;5;205m\u001b[97m"
    };
}
