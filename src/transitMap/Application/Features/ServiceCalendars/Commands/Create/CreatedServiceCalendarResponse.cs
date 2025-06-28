using Shared.Application.Responses;

namespace Application.Features.ServiceCalendars.Commands.Create;

public class CreatedServiceCalendarResponse : IResponse
{
    public Guid Id { get; set; }
    public bool Monday { get; set; }
    public bool Tuesday { get; set; }
    public bool Wednesday { get; set; }
    public bool Thursday { get; set; }
    public bool Friday { get; set; }
    public bool Saturday { get; set; }
    public bool Sunday { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}