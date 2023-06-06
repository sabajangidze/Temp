using Application.EventManagement.Dto;
using Domain.Interfaces.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Extensions;

namespace Application.EventManagement.Queries.GetEvents
{
    public class GetEventsHandler : IRequestHandler<GetEventsRequest, GetEventsResponse>
    {
        private readonly IEventRepository _eventRepository;

        public GetEventsHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<GetEventsResponse> Handle(GetEventsRequest request, CancellationToken cancellationToken)
        {
            var events = _eventRepository.Query().Include(x => x.User);

            var total = events.Count();

            var eventsList = await events.Pagination(request).ToListAsync();

            var response = new GetEventsResponse
            {
                Events = eventsList.Select(x => new EventDtoModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Date = x.Date,
                    ReservedCount = x.ReservedCount,
                    PlaceCount = x.PlaceCount,
                    UserId = x.UserId,
                }),
                Page = request.Page,
                PageSize = request.PageSize,
                Total = total,
            };

            return response;
        }
    }
}