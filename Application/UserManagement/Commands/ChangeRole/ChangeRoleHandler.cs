using Domain.Interfaces.Repository;
using MediatR;

namespace Application.UserManagement.Commands.ChangeRole
{
    public class ChangeRoleHandler : IRequestHandler<ChangeRole>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public ChangeRoleHandler(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task<Unit> Handle(ChangeRole request, CancellationToken cancellationToken)
        {
            var newRole = await _roleRepository.OfIdAsync(request.NewRoleId.ToString());

            if (newRole == null)
            {
                throw new KeyNotFoundException($"Role was not found for Id: {request.NewRoleId}");
            }

            var user = await _userRepository.OfIdAsync(request.UserId.ToString());

            if (user == null)
            {
                throw new KeyNotFoundException($"User was not found for Id: {request.UserId}");
            }

            var existingRoles = await _userRepository.GetUserRolesAsync(user);

            if (existingRoles.Any())
            {
                foreach (var role in existingRoles)
                {
                    await _userRepository.RemoveFromRoleAsync(user, role);
                }
            }
            
            var result = await _userRepository.AddToRoleAsync(user, newRole);

            if (!result.Succeeded)
            {
                throw new Exception("Failed to assign newRole.");
            }

            return Unit.Value;
        }
    }
}