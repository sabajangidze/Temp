using FluentValidation;

namespace Application.RoleClaimManagement.Commands.CreateRoleClaim
{
    public class CreateRoleClaimValidator : AbstractValidator<CreateRoleClaim>
    {
        public CreateRoleClaimValidator()
        {
            RuleFor(x => x.ClaimType).NotEmpty();
            RuleFor(x => x.ClaimValue).NotEmpty();
        }
    }
}