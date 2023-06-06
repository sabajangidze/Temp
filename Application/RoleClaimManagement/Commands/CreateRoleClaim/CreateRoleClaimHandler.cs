using Domain.Interfaces.Repository;
using MediatR;
using System.Security.Claims;

namespace Application.RoleClaimManagement.Commands.CreateRoleClaim
{
    public class CreateRoleClaimHandler : IRequestHandler<CreateRoleClaim>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IRoleClaimRepository _roleClaimRepository;

        public CreateRoleClaimHandler(IRoleRepository roleRepository, IRoleClaimRepository roleClaimRepository)
        {
            _roleRepository = roleRepository;
            _roleClaimRepository = roleClaimRepository;
        }

        public async Task<Unit> Handle(CreateRoleClaim request, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.OfIdAsync(request.RoleId.ToString());

            if (role == null)
            {
                throw new KeyNotFoundException($"Role was not found for Id: {request.RoleId}");
            }

            var claim = new Claim(request.ClaimType, request.ClaimValue);

            var result = await _roleClaimRepository.InsertAsync(role, claim);

            if (!result.Succeeded)
            {
                throw new Exception("Failed to create role claim.");
            }

            return Unit.Value;
        }
    }
}