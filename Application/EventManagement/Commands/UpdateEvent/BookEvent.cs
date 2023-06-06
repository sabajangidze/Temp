using MediatR;

namespace Application.EventManagement.Commands.UpdateEvent
{
    public record BookEvent(Guid Id, int ReservedCount, bool Status) : IRequest;
}