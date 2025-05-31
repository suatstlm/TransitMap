using FluentValidation;

namespace Application.Features.Shapes.Commands.Update;

public class UpdateShapeCommandValidator : AbstractValidator<UpdateShapeCommand>
{
    public UpdateShapeCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.TripId).NotEmpty();
        RuleFor(c => c.ShapeLat).NotEmpty();
        RuleFor(c => c.ShapeLon).NotEmpty();
        RuleFor(c => c.ShapePtSequence).NotEmpty();
        RuleFor(c => c.Trip).NotEmpty();
    }
}