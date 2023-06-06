using MediatR;

namespace Application.RoleClaimManagement.Commands.CreateRoleClaim
{
    public record CreateRoleClaim(string ClaimType, string ClaimValue, Guid RoleId) : IRequest;
}