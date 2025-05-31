using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;

/// <summary>
/// Represents the path a vehicle takes for a trip, defined by a series of points.
/// </summary>
public class Shape : Entity<Guid>
{
    public Guid TripId { get; set; }
    public double ShapeLat { get; set; }
    public double ShapeLon { get; set; }
    public int ShapePtSequence { get; set; }

    public Trip Trip { get; set; }
}


