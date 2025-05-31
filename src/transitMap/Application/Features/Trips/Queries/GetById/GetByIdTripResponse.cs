using Domain.Entities;
using NArchitecture.Core.Application.Responses;

namespace Application.Features.Trips.Queries.GetById;

public class GetByIdTripResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid RouteId { get; set; }
    public Guid ServiceId { get; set; }
    public string TripHeadsign { get; set; }
    public Route Route { get; set; }
    public ServiceCalendar ServiceCalendar { get; set; }
}