using Domain.Entities;
using Shared.Application.Responses;

namespace Application.Features.Shapes.Commands.Update;

public class UpdatedShapeResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid TripId { get; set; }
    public double ShapeLat { get; set; }
    public double ShapeLon { get; set; }
    public int ShapePtSequence { get; set; }
    public Trip Trip { get; set; }
}