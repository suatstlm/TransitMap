using FluentValidation;

namespace Application.Features.Routes.Commands.Create;

public class CreateRouteCommandValidator : AbstractValidator<CreateRouteCommand>
{
    public CreateRouteCommandValidator()
    {
        RuleFor(c => c.AgencyId).NotEmpty();
        RuleFor(c => c.RouteShortName).NotEmpty();
        RuleFor(c => c.RouteLongName).NotEmpty();
        RuleFor(c => c.RouteType).NotEmpty();
        RuleFor(c => c.Agency).NotEmpty();
    }
}