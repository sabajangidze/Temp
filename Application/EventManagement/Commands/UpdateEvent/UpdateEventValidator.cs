using FluentValidation;

namespace Application.EventManagement.Commands.UpdateEvent
{
    public class UpdateEventValidator : AbstractValidator<UpdateEvent>
    {
        public UpdateEventValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.Date).NotNull().NotEmpty();
            RuleFor(x => x.ReservedCount).NotNull().NotEmpty();
            RuleFor(x => x.PlaceCount).NotNull().NotEmpty();
        }
    }
}