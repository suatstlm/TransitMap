using FluentValidation;

namespace Application.Features.Agencies.Commands.Create;

public class CreateAgencyCommandValidator : AbstractValidator<CreateAgencyCommand>
{
    public CreateAgencyCommandValidator()
    {
        RuleFor(c => c.AgencyName).NotEmpty();
        RuleFor(c => c.AgencyUrl).NotEmpty();
        RuleFor(c => c.AgencyTimezone).NotEmpty();
        RuleFor(c => c.AgencyLang).NotEmpty();
        RuleFor(c => c.AgencyPhone).NotEmpty();
    }
}