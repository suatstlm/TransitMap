using FluentValidation;

namespace Application.Features.StopTimes.Commands.Delete;

public class DeleteStopTimeCommandValidator : AbstractValidator<DeleteStopTimeCommand>
{
    public DeleteStopTimeCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}