using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Stops.Queries.GetList;

public class GetListStopListItemDto : IDto
{
    public Guid Id { get; set; }
    public string StopName { get; set; }
    public double StopLat { get; set; }
    public double StopLon { get; set; }
}