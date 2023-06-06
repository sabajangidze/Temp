using MediatR;

namespace Application.RoleManagement.Commands.UpdateRole
{
    public record UpdateRole(Guid Id, string Name, string? Description) : IRequest;
}