using Domain.Interfaces.DataAccess;
using Domain.Interfaces.Repository;
using MediatR;

namespace Application.EventManagement.Commands.UpdateEvent
{
    public class UpdateEventHandler : IRequestHandler<UpdateEvent>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateEventHandler(IEventRepository eventRepository, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _eventRepository = eventRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateEvent request, CancellationToken cancellationToken)
        {
            var eventModel = await _eventRepository.OfIdAsync(request.Id);

            if (eventModel == null)
            {
                throw new KeyNotFoundException($"Event not found for Id: {request.Id}");
            }

            eventModel.Name = request.Name;
            eventModel.Date = request.Date;
            eventModel.ReservedCount = request.ReservedCount;
            eventModel.PlaceCount = request.PlaceCount;

            _eventRepository.Update(eventModel);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}