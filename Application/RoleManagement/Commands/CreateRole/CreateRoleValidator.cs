using FluentValidation;

namespace Application.RoleManagement.Commands.CreateRole
{
    public class CreateRoleValidator : AbstractValidator<CreateRole>
    {
        public CreateRoleValidator()
        {
            RuleFor(x => x.Name).MinimumLength(3);
        }
    }
}