using FluentValidation;

namespace Application.Features.Shapes.Commands.Create;

public class CreateShapeCommandValidator : AbstractValidator<CreateShapeCommand>
{
    public CreateShapeCommandValidator()
    {
        RuleFor(c => c.TripId).NotEmpty();
        RuleFor(c => c.ShapeLat).NotEmpty();
        RuleFor(c => c.ShapeLon).NotEmpty();
        RuleFor(c => c.ShapePtSequence).NotEmpty();
        RuleFor(c => c.Trip).NotEmpty();
    }
}