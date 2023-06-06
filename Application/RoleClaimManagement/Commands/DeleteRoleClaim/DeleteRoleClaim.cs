#nullable disable

using MediatR;

namespace Application.RoleClaimManagement.Commands.DeleteRoleClaim
{
    public class DeleteRoleClaim : IRequest
    {
        public int RoleClaimId { get; set; }
    }
}