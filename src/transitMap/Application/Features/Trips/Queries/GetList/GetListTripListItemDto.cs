using Domain.Entities;
using Shared.Application.Dtos;

namespace Application.Features.Trips.Queries.GetList;

public class GetListTripListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid RouteId { get; set; }
    public Guid ServiceId { get; set; }
    public string TripHeadsign { get; set; }
    public Route Route { get; set; }
    public ServiceCalendar ServiceCalendar { get; set; }
}