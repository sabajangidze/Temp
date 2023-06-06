using MediatR;

namespace Application.EventManagement.Commands.DeleteEvent
{
    public class DeleteEvent : IRequest
    {
        public Guid EventId { get; set; }
    }
}