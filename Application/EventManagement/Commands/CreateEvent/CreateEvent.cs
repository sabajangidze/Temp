using MediatR;

namespace Application.EventManagement.Commands.CreateEvent
{
    public record CreateEvent(string Name, DateTime Date, int ReservedCount, int PlaceCount, Guid UserId) : IRequest;
}