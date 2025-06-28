using Domain.Entities;
using Shared.Application.Responses;

namespace Application.Features.Trips.Commands.Create;

public class CreatedTripResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid RouteId { get; set; }
    public Guid ServiceId { get; set; }
    public string TripHeadsign { get; set; }
    public Route Route { get; set; }
    public ServiceCalendar ServiceCalendar { get; set; }
}