using Domain.Interfaces.Repository;
using MediatR;

namespace Application.UserManagement.Commands.DeleteUser
{
    public class DeleteUserHandler : IRequestHandler<DeleteUser>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Unit> Handle(DeleteUser request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.OfIdAsync(request.UserId.ToString());

            if (user == null)
            {
                throw new KeyNotFoundException($"User was not found for Id: {request.UserId}");
            }

            var result = await _userRepository.DeleteAsync(user);

            if (!result.Succeeded)
            {
                throw new Exception("Failed to delete user.");
            }

            return Unit.Value;
        }
    }
}