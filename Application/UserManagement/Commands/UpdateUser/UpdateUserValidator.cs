using FluentValidation;

namespace Application.UserManagement.Commands.UpdateUser
{
    public class UpdateUserValidator : AbstractValidator<UpdateUser>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.Email).EmailAddress();
        }
    }
}