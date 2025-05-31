using FluentValidation;

namespace Application.Features.Stops.Commands.Create;

public class CreateStopCommandValidator : AbstractValidator<CreateStopCommand>
{
    public CreateStopCommandValidator()
    {
        RuleFor(c => c.StopName).NotEmpty();
        RuleFor(c => c.StopLat).NotEmpty();
        RuleFor(c => c.StopLon).NotEmpty();
    }
}