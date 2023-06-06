using Domain.Entities;
using Domain.Interfaces.Repository;
using MediatR;

namespace Application.RoleManagement.Commands.CreateRole
{
    public class CreateRoleHandler : IRequestHandler<CreateRole>
    {
        private readonly IRoleRepository _roleRepository;

        public CreateRoleHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<Unit> Handle(CreateRole request, CancellationToken cancellationToken)
        {
            var role = new Role
            {
                Name = request.Name,
                Description = request.Description,
            };

            var result = await _roleRepository.CreateAsync(role);

            if (!result.Succeeded)
            {
                throw new Exception("Failed to create role.");
            }

            return Unit.Value;
        }
    }
}