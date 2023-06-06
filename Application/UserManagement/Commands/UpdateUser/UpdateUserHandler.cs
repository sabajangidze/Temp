using Domain.Interfaces.Repository;
using MediatR;

namespace Application.UserManagement.Commands.UpdateUser
{
    public class UpdateUserHandler : IRequestHandler<UpdateUser>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(UpdateUser request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.OfIdAsync(request.Id.ToString());

            if (user == null)
            {
                throw new KeyNotFoundException($"User was not found for Id: {request.Id}");
            }

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.PhoneNumber = request.PhoneNumber;

            await _userRepository.UpdateAsync(user);

            return Unit.Value;
        }
    }
}