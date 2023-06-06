using FluentValidation;

namespace Application.EventManagement.Commands.CreateEvent
{
    public class CreateEventValidator : AbstractValidator<CreateEvent>
    {
        public CreateEventValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.Date).NotNull().NotEmpty();
            RuleFor(x => x.ReservedCount).NotNull().NotEmpty();
            RuleFor(x => x.PlaceCount).NotNull().NotEmpty();
        }
    }
}