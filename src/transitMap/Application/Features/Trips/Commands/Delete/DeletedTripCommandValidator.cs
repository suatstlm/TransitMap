using FluentValidation;

namespace Application.Features.Trips.Commands.Delete;

public class DeleteTripCommandValidator : AbstractValidator<DeleteTripCommand>
{
    public DeleteTripCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}