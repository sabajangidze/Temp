#nullable disable

using Application.Shared;
using Application.EventManagement.Dto;

namespace Application.EventManagement.Queries.GetEvents
{
    public class GetEventsResponse : PaginationResponse
    {
        public IEnumerable<EventDtoModel> Events { get; set; }
    }
}