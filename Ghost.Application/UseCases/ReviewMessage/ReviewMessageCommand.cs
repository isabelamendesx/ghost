using MediatR;

namespace Ghost.Application.UseCases.ReviewMessage;

public record ReviewMessageCommand(string message) : IRequest<bool>;