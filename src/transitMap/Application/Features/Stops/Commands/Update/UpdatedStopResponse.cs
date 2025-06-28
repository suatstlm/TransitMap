using Shared.Application.Responses;

namespace Application.Features.Stops.Commands.Update;

public class UpdatedStopResponse : IResponse
{
    public Guid Id { get; set; }
    public string StopName { get; set; }
    public double StopLat { get; set; }
    public double StopLon { get; set; }
}