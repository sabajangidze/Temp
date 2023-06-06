using FluentValidation;

namespace Application.UserManagement.Commands.CreateUser
{
    public class CreateUserValidator : AbstractValidator<CreateUser>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.Email).EmailAddress();
        }
    }
}