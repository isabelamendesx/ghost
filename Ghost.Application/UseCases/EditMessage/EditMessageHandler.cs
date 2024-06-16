using MediatR;
using System.Text;

namespace Ghost.Application.UseCases.EditMessage;

public class EditMessageHandler : IRequestHandler<EditMessageCommand, string>
{
    public Task<string> Handle(EditMessageCommand request, CancellationToken cancellationToken)
    {
        Console.Clear();
       Console.WriteLine("👻 Edit the message as you wish:");
        return Task.FromResult(EditString(request.Message));
    }

    private static string EditString(string initialText)
    {
        var buffer = new StringBuilder(initialText);
        int cursorPosition = buffer.Length;

        Console.Write(initialText);

        while (true)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            switch (keyInfo.Key)
            {
                case ConsoleKey.LeftArrow:
                    if (cursorPosition > 0)
                    {
                        cursorPosition--;
                        Console.SetCursorPosition(cursorPosition, Console.CursorTop);
                    }
                    break;

                case ConsoleKey.RightArrow:
                    if (cursorPosition < buffer.Length)
                    {
                        cursorPosition++;
                        Console.SetCursorPosition(cursorPosition, Console.CursorTop);
                    }
                    break;

                case ConsoleKey.Backspace:
                    if (cursorPosition > 0)
                    {
                        buffer.Remove(cursorPosition - 1, 1);
                        cursorPosition--;
                        RedrawBuffer(buffer, cursorPosition);
                    }
                    break;

                case ConsoleKey.Delete:
                    if (cursorPosition < buffer.Length)
                    {
                        buffer.Remove(cursorPosition, 1);
                        RedrawBuffer(buffer, cursorPosition);
                    }
                    break;

                case ConsoleKey.Enter:
                    Console.WriteLine();
                    return buffer.ToString();

                default:
                    if (!char.IsControl(keyInfo.KeyChar))
                    {
                        buffer.Insert(cursorPosition, keyInfo.KeyChar);
                        cursorPosition++;
                        RedrawBuffer(buffer, cursorPosition);
                    }
                    break;
            }
        }
    }

    static void RedrawBuffer(StringBuilder buffer, int cursorPosition)
    {
        Console.SetCursorPosition(0, Console.CursorTop);
        Console.Write(buffer.ToString() + " ");
        Console.SetCursorPosition(cursorPosition, Console.CursorTop);
    }
}
