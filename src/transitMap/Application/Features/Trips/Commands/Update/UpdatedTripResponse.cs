using Domain.Entities;
using Shared.Application.Responses;

namespace Application.Features.Trips.Commands.Update;

public class UpdatedTripResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid RouteId { get; set; }
    public Guid ServiceId { get; set; }
    public string TripHeadsign { get; set; }
    public Route Route { get; set; }
    public ServiceCalendar ServiceCalendar { get; set; }
}