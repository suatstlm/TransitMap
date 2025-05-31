using Domain.Entities;
using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Shapes.Queries.GetList;

public class GetListShapeListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid TripId { get; set; }
    public double ShapeLat { get; set; }
    public double ShapeLon { get; set; }
    public int ShapePtSequence { get; set; }
    public Trip Trip { get; set; }
}