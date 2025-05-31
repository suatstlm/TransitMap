using FluentValidation;

namespace Application.Features.Agencies.Commands.Delete;

public class DeleteAgencyCommandValidator : AbstractValidator<DeleteAgencyCommand>
{
    public DeleteAgencyCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}