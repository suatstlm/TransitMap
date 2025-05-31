using FluentValidation;

namespace Application.Features.Routes.Commands.Update;

public class UpdateRouteCommandValidator : AbstractValidator<UpdateRouteCommand>
{
    public UpdateRouteCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.AgencyId).NotEmpty();
        RuleFor(c => c.RouteShortName).NotEmpty();
        RuleFor(c => c.RouteLongName).NotEmpty();
        RuleFor(c => c.RouteType).NotEmpty();
        RuleFor(c => c.Agency).NotEmpty();
    }
}