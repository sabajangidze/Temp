using Application.RoleClaimManagement.Dto;
using Domain.Interfaces.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Extensions;

namespace Application.RoleClaimManagement.Queries.GetRoleClaims
{
    public class GetRoleClaimsHandler : IRequestHandler<GetRoleClaimsRequest, GetRoleClaimsResponse>
    {
        private readonly IRoleClaimRepository _roleClaimRepository;
        private readonly IRoleRepository _roleRepository;

        public GetRoleClaimsHandler(IRoleClaimRepository roleClaimRepository, IRoleRepository roleRepository)
        {
            _roleClaimRepository = roleClaimRepository;
            _roleRepository = roleRepository;
        }

        public async Task<GetRoleClaimsResponse> Handle(GetRoleClaimsRequest request, CancellationToken cancellationToken)
        {
            if (request.RoleId != default)
            {
                var role = await _roleRepository.OfIdAsync(request.RoleId.ToString()!);

                if (role == null)
                {
                    throw new KeyNotFoundException($"Role was not found for Id: {request.RoleId}");
                }
            }

            var roleClaims = request.RoleId != default ? _roleClaimRepository.Query(x => x.RoleId == request.RoleId.ToString()) : _roleClaimRepository.Query();

            var total = roleClaims.Count();

            var roleClaimsList = await roleClaims.Pagination(request).ToListAsync();

            var response = new GetRoleClaimsResponse
            {
                RoleClaims = roleClaimsList.Select(x => new RoleClaimDtoModel
                {
                    Id = x.Id,
                    ClaimType = x.ClaimType,
                    ClaimValue = x.ClaimValue,
                }),
                Page = request.Page,
                PageSize = request.PageSize,
                Total = total,
            };

            return response;
        }
    }
}