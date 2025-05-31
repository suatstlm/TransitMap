using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;

/// <summary>
/// Represents a route that groups trips and defines a transit line.
/// </summary>
public class Route : Entity<Guid>
{
    public Guid AgencyId { get; set; }
    public string RouteShortName { get; set; }
    public string RouteLongName { get; set; }
    public int RouteType { get; set; }

    public Agency Agency { get; set; }
    public ICollection<Trip> Trips { get; set; }
}


