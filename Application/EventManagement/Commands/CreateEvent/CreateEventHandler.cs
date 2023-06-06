using Domain.Entities;
using Domain.Interfaces.DataAccess;
using Domain.Interfaces.Repository;
using MediatR;

namespace Application.EventManagement.Commands.CreateEvent
{
    public class CreateEventHandler : IRequestHandler<CreateEvent>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateEventHandler(IEventRepository eventRepository, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _eventRepository = eventRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(CreateEvent request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.OfIdAsync(request.UserId.ToString());

            if (user == null)
            {
                throw new KeyNotFoundException($"User not found for Id: {request.UserId}");
            }

            var eventModel = new Event()
            {
                Name = request.Name,
                Date = request.Date,
                ReservedCount = request.ReservedCount,
                PlaceCount = request.PlaceCount,
                User = user,
            };

            await _eventRepository.InsertAsync(eventModel);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}