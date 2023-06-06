using FluentValidation;

namespace Application.RoleManagement.Commands.UpdateRole
{
    public class UpdateRoleValidator : AbstractValidator<UpdateRole>
    {
        public UpdateRoleValidator()
        {
            RuleFor(x => x.Name).MinimumLength(3);
        }
    }
}