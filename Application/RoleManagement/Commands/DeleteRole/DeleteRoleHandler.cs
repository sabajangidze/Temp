using Domain.Interfaces.Repository;
using MediatR;

namespace Application.RoleManagement.Commands.DeleteRole
{
    public class DeleteRoleHandler : IRequestHandler<DeleteRole>
    {
        private readonly IRoleRepository _roleRepository;

        public DeleteRoleHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<Unit> Handle(DeleteRole request, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.OfIdAsync(request.RoleId.ToString());

            if (role == null)
            {
                throw new KeyNotFoundException($"Role was not found for Id: {request.RoleId}");
            }

            var result = await _roleRepository.DeleteAsync(role);

            if (!result.Succeeded)
            {
                throw new Exception("Failed to delete role.");
            }

            return Unit.Value;
        }
    }
}