using Application.EventManagement.Queries.GetEvents;
using Application.Shared;
using MediatR;

namespace Application.EventManagement.Queries.GetEvents
{
    public class GetEventsRequest : PaginationRequest, IRequest<GetEventsResponse>
    {
    }
}