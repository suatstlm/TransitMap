using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;

/// <summary>
/// Represents a transit stop where vehicles pick up or drop off passengers.
/// </summary>
public class Stop : Entity<Guid>
{
    public string StopName { get; set; }
    public double StopLat { get; set; }
    public double StopLon { get; set; }

    public ICollection<StopTime> StopTimes { get; set; }
}


