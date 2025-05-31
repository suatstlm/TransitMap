using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;

/// <summary>
/// Represents a transit agency responsible for managing routes.
/// </summary>
public class Agency : Entity<Guid>
{
    public string AgencyName { get; set; }
    public string AgencyUrl { get; set; }
    public string AgencyTimezone { get; set; }
    public string AgencyLang { get; set; }
    public string AgencyPhone { get; set; }

    public ICollection<Route> Routes { get; set; }
}


