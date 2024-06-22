using MediatR;

namespace Ghost.Application.UseCases.PickMessage;

public class PickMessageCommand : IRequest<bool>
{
    public List<string> Messages { get; set; } = [];
}
