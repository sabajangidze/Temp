#nullable disable

using MediatR;

namespace Application.RoleManagement.Queries.GetRole
{
    public class GetRoleRequest : IRequest<GetRoleResponse>
    {
        public Guid RoleId { get; set; }
    }
}