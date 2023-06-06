using Domain.Interfaces.DataAccess;
using Domain.Interfaces.Repository;
using MediatR;

namespace Application.EventManagement.Commands.DeleteEvent
{
    public class DeleteEventHandler : IRequestHandler<DeleteEvent>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteEventHandler(IEventRepository eventRepository, IUnitOfWork unitOfWork)
        {
            _eventRepository = eventRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteEvent request, CancellationToken cancellationToken)
        {
            var eventModel = await _eventRepository.OfIdAsync(request.EventId);

            if (eventModel == null)
            {
                throw new KeyNotFoundException($"Event not found for Id: {request.EventId}");
            }

            _eventRepository.Delete(eventModel);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}