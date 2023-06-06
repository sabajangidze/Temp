using Application.EventManagement.Dto;
using Application.EventManagement.Queries.GetEvent;
using Application.UserManagement.Dto;
using Domain.Interfaces.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.EventManagement.Queries.GetEvent
{
    public class GetEventHandler : IRequestHandler<GetEventRequest, GetEventResponse>
    {
        private readonly IEventRepository _eventRepository;

        public GetEventHandler(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<GetEventResponse> Handle(GetEventRequest request, CancellationToken cancellationToken)
        {
            var eventModel = await _eventRepository.Query(x => x.Id == request.EventId)
                                            .Include(x => x.User)
                                            .FirstOrDefaultAsync();

            if (eventModel == default)
            {
                throw new KeyNotFoundException($"Event was not found for Id: {request.EventId}");
            }

            return new GetEventResponse
            {
                Event = new EventDtoModel
                {
                    Id = eventModel.Id,
                    Name = eventModel.Name,
                    Date = eventModel.Date,
                    ReservedCount = eventModel.ReservedCount,
                    PlaceCount = eventModel.PlaceCount,
                    UserId = eventModel.UserId,
                },
            };
        }
    }
}