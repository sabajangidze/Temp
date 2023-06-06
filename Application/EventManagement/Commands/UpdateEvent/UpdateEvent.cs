using MediatR;

namespace Application.EventManagement.Commands.UpdateEvent
{
    public record UpdateEvent(Guid Id, string Name, DateTime Date, int ReservedCount, int PlaceCount) : IRequest;
}