using Application.Shared;
using MediatR;

namespace Application.RoleClaimManagement.Queries.GetRoleClaims
{
    public class GetRoleClaimsRequest : PaginationRequest, IRequest<GetRoleClaimsResponse>
    {
        public Guid? RoleId { get; set; }
    }
}