using FluentValidation;

namespace Application.Features.ServiceCalendars.Commands.Create;

public class CreateServiceCalendarCommandValidator : AbstractValidator<CreateServiceCalendarCommand>
{
    public CreateServiceCalendarCommandValidator()
    {
        RuleFor(c => c.Monday).NotEmpty();
        RuleFor(c => c.Tuesday).NotEmpty();
        RuleFor(c => c.Wednesday).NotEmpty();
        RuleFor(c => c.Thursday).NotEmpty();
        RuleFor(c => c.Friday).NotEmpty();
        RuleFor(c => c.Saturday).NotEmpty();
        RuleFor(c => c.Sunday).NotEmpty();
        RuleFor(c => c.StartDate).NotEmpty();
        RuleFor(c => c.EndDate).NotEmpty();
    }
}