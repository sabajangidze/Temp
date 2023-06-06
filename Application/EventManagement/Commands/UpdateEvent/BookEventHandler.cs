using Domain.Interfaces.DataAccess;
using Domain.Interfaces.Repository;
using MediatR;

namespace Application.EventManagement.Commands.UpdateEvent
{
    public class BookEventHandler : IRequestHandler<BookEvent>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BookEventHandler(IEventRepository eventRepository, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _eventRepository = eventRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(BookEvent request, CancellationToken cancellationToken)
        {
            var eventModel = await _eventRepository.OfIdAsync(request.Id);

            if (eventModel == null)
            {
                throw new KeyNotFoundException($"Event not found for Id: {request.Id}");
            }

            if (request.Status)
            {
                eventModel.ReservedCount = eventModel.PlaceCount - eventModel.ReservedCount - request.ReservedCount;
            }
            else
            {
                eventModel.ReservedCount = eventModel.PlaceCount - eventModel.ReservedCount + request.ReservedCount;
            }

            _eventRepository.Update(eventModel);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}