using FluentValidation;

namespace Application.Features.Trips.Commands.Create;

public class CreateTripCommandValidator : AbstractValidator<CreateTripCommand>
{
    public CreateTripCommandValidator()
    {
        RuleFor(c => c.RouteId).NotEmpty();
        RuleFor(c => c.ServiceId).NotEmpty();
        RuleFor(c => c.TripHeadsign).NotEmpty();
        RuleFor(c => c.Route).NotEmpty();
        RuleFor(c => c.ServiceCalendar).NotEmpty();
    }
}