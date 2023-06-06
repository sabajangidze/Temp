using MediatR;

namespace Application.IdentityManagement.Commands.CreateAuthenticationToken
{
    public record class CreateAuthenticationTokenRequest(string Username, string Password) : IRequest<CreateAuthenticationTokenResponse>;
}
