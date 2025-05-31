using FluentValidation;

namespace Application.Features.Trips.Commands.Update;

public class UpdateTripCommandValidator : AbstractValidator<UpdateTripCommand>
{
    public UpdateTripCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.RouteId).NotEmpty();
        RuleFor(c => c.ServiceId).NotEmpty();
        RuleFor(c => c.TripHeadsign).NotEmpty();
        RuleFor(c => c.Route).NotEmpty();
        RuleFor(c => c.ServiceCalendar).NotEmpty();
    }
}