using MediatR;

namespace Application.UserManagement.Commands.ChangeRole
{
    public record ChangeRole(Guid UserId, Guid NewRoleId) : IRequest;
}