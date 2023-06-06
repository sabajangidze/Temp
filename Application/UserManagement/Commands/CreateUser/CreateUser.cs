using MediatR;

namespace Application.UserManagement.Commands.CreateUser
{
    public record CreateUser(string? FirstName, string? LastName, string Email, string? PhoneNumber, string Password, Guid RoleId) : IRequest;
}