using FluentValidation;

namespace Application.Features.StopTimes.Commands.Update;

public class UpdateStopTimeCommandValidator : AbstractValidator<UpdateStopTimeCommand>
{
    public UpdateStopTimeCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.TripId).NotEmpty();
        RuleFor(c => c.StopId).NotEmpty();
        RuleFor(c => c.ArrivalTime).NotEmpty();
        RuleFor(c => c.DepartureTime).NotEmpty();
        RuleFor(c => c.StopSequence).NotEmpty();
        RuleFor(c => c.Trip).NotEmpty();
        RuleFor(c => c.Stop).NotEmpty();
    }
}