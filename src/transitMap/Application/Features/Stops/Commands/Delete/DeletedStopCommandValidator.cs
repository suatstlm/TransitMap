using FluentValidation;

namespace Application.Features.Stops.Commands.Delete;

public class DeleteStopCommandValidator : AbstractValidator<DeleteStopCommand>
{
    public DeleteStopCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}