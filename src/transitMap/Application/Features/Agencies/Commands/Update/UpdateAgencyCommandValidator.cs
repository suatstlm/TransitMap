using FluentValidation;

namespace Application.Features.Agencies.Commands.Update;

public class UpdateAgencyCommandValidator : AbstractValidator<UpdateAgencyCommand>
{
    public UpdateAgencyCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.AgencyName).NotEmpty();
        RuleFor(c => c.AgencyUrl).NotEmpty();
        RuleFor(c => c.AgencyTimezone).NotEmpty();
        RuleFor(c => c.AgencyLang).NotEmpty();
        RuleFor(c => c.AgencyPhone).NotEmpty();
    }
}