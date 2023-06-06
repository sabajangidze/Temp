using Domain.Interfaces.Repository;
using MediatR;

namespace Application.RoleManagement.Commands.UpdateRole
{
    public class UpdateRoleHandler : IRequestHandler<UpdateRole>
    {
        private readonly IRoleRepository _roleRepository;

        public UpdateRoleHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<Unit> Handle(UpdateRole request, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.OfIdAsync(request.Id.ToString());

            if (role == null)
            {
                throw new KeyNotFoundException($"Role was not found for Id: {request.Id}");
            }

            role.Name = request.Name;
            role.Description = request.Description;

            var result = await _roleRepository.UpdateAsync(role);

            if (!result.Succeeded)
            {
                throw new Exception("Failed to update role.");
            }

            return Unit.Value;
        }
    }
}