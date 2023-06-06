using Application.RoleManagement.Dto;
using Domain.Interfaces.Repository;
using MediatR;

namespace Application.RoleManagement.Queries.GetRole
{
    public class GetRoleHandler : IRequestHandler<GetRoleRequest, GetRoleResponse>
    {
        private readonly IRoleRepository _roleRepository;

        public GetRoleHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<GetRoleResponse> Handle(GetRoleRequest request, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.OfIdAsync(request.RoleId.ToString());

            if (role == null)
            {
                throw new KeyNotFoundException($"Role was not found for Id: {request.RoleId}");
            }

            return new GetRoleResponse
            {
                Role = new RoleDtoModel
                {
                    Id = role.Id,
                    Name = role.Name,
                    Description = role.Description,
                },
            };
        }
    }
}