using FluentValidation;

namespace Application.Features.ServiceCalendars.Commands.Delete;

public class DeleteServiceCalendarCommandValidator : AbstractValidator<DeleteServiceCalendarCommand>
{
    public DeleteServiceCalendarCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}