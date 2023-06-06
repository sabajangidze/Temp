#nullable disable

using MediatR;

namespace Application.RoleManagement.Commands.DeleteRole
{
    public class DeleteRole : IRequest
    {
        /// <summary>
        /// Gets or Sets UserId
        /// </summary>
        public Guid RoleId { get; set; }
    }
}