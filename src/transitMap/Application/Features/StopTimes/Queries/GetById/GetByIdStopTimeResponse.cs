using Domain.Entities;
using NArchitecture.Core.Application.Responses;

namespace Application.Features.StopTimes.Queries.GetById;

public class GetByIdStopTimeResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid TripId { get; set; }
    public Guid StopId { get; set; }
    public TimeSpan ArrivalTime { get; set; }
    public TimeSpan DepartureTime { get; set; }
    public int StopSequence { get; set; }
    public Trip Trip { get; set; }
    public Stop Stop { get; set; }
}