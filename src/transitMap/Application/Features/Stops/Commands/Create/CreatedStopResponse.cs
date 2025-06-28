using Shared.Application.Responses;

namespace Application.Features.Stops.Commands.Create;

public class CreatedStopResponse : IResponse
{
    public Guid Id { get; set; }
    public string StopName { get; set; }
    public double StopLat { get; set; }
    public double StopLon { get; set; }
}