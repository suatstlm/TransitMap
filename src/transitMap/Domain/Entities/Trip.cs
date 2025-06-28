using Shared.Persistence.Repositories;

namespace Domain.Entities;

/// <summary>
/// Represents a scheduled trip for a given route and service.
/// </summary>
public class Trip : Entity<Guid>
{
    public Guid RouteId { get; set; }
    public Guid ServiceId { get; set; }
    public string TripHeadsign { get; set; }

    public Route Route { get; set; }
    public ServiceCalendar ServiceCalendar { get; set; }
    public ICollection<StopTime> StopTimes { get; set; }
    public ICollection<Shape> ShapePoints { get; set; }
}


