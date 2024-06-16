using MediatR;

namespace Ghost.Application.UseCases.EditMessage;

public class EditMessageCommand : IRequest<string>
{
    public string Message { get; set; } = string.Empty;
}
