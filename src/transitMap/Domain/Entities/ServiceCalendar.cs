using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;

/// <summary>
/// Represents the calendar of days on which a service is available.
/// </summary>
public class ServiceCalendar : Entity<Guid>
{
    public bool Monday { get; set; }
    public bool Tuesday { get; set; }
    public bool Wednesday { get; set; }
    public bool Thursday { get; set; }
    public bool Friday { get; set; }
    public bool Saturday { get; set; }
    public bool Sunday { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public ICollection<Trip> Trips { get; set; }
}


