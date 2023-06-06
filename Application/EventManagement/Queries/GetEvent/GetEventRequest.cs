#nullable disable

using MediatR;

namespace Application.EventManagement.Queries.GetEvent
{
    public class GetEventRequest : IRequest<GetEventResponse>
    {
        public Guid EventId { get; set; }
    }
}