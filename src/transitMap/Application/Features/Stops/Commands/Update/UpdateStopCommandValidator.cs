using FluentValidation;

namespace Application.Features.Stops.Commands.Update;

public class UpdateStopCommandValidator : AbstractValidator<UpdateStopCommand>
{
    public UpdateStopCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.StopName).NotEmpty();
        RuleFor(c => c.StopLat).NotEmpty();
        RuleFor(c => c.StopLon).NotEmpty();
    }
}