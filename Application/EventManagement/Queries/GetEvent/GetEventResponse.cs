#nullable disable

using Application.EventManagement.Dto;

namespace Application.EventManagement.Queries.GetEvent
{
    public class GetEventResponse
    {
        public EventDtoModel Event { get; set; }
    }
}