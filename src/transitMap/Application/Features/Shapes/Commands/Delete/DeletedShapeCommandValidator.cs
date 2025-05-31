using FluentValidation;

namespace Application.Features.Shapes.Commands.Delete;

public class DeleteShapeCommandValidator : AbstractValidator<DeleteShapeCommand>
{
    public DeleteShapeCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}