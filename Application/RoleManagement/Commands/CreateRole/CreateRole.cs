using MediatR;

namespace Application.RoleManagement.Commands.CreateRole
{
    public record CreateRole(string Name, string? Description) : IRequest;
}