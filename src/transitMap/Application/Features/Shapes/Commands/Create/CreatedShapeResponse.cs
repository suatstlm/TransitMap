using Domain.Entities;
using NArchitecture.Core.Application.Responses;

namespace Application.Features.Shapes.Commands.Create;

public class CreatedShapeResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid TripId { get; set; }
    public double ShapeLat { get; set; }
    public double ShapeLon { get; set; }
    public int ShapePtSequence { get; set; }
    public Trip Trip { get; set; }
}