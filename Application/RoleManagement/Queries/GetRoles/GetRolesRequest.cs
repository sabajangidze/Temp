using Application.Shared;
using MediatR;

namespace Application.RoleManagement.Queries.GetRoles
{
    public class GetRolesRequest : PaginationRequest, IRequest<GetRolesResponse>
    {
    }
}