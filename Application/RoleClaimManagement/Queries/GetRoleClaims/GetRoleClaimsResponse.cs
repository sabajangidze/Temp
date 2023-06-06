#nullable disable

using Application.RoleClaimManagement.Dto;
using Application.Shared;

namespace Application.RoleClaimManagement.Queries.GetRoleClaims
{
    public class GetRoleClaimsResponse : PaginationResponse
    {
        public IEnumerable<RoleClaimDtoModel> RoleClaims { get; set; }
    }
}