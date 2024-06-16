using MediatR;

namespace Ghost.Application.UseCases.PickMessage;

public class PickMessageCommand : IRequest<string>
{
    public List<string> Messages { get; set; } = [];
}
