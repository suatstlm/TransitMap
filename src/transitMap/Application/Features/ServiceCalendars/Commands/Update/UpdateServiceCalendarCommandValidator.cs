using FluentValidation;

namespace Application.Features.ServiceCalendars.Commands.Update;

public class UpdateServiceCalendarCommandValidator : AbstractValidator<UpdateServiceCalendarCommand>
{
    public UpdateServiceCalendarCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
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