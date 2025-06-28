using Shared.Persistence.Repositories;

namespace Domain.Entities;

/// <summary>
/// Represents the scheduled arrival and departure times of a trip at each stop.
/// </summary>
public class StopTime : Entity<Guid>
{
    public Guid TripId { get; set; }
    public Guid StopId { get; set; }
    public TimeSpan ArrivalTime { get; set; }
    public TimeSpan DepartureTime { get; set; }
    public int StopSequence { get; set; }

    public Trip Trip { get; set; }
    public Stop Stop { get; set; }
}


