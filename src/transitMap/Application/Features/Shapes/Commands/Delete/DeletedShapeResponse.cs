using Shared.Application.Responses;

namespace Application.Features.Shapes.Commands.Delete;

public class DeletedShapeResponse : IResponse
{
    public Guid Id { get; set; }
}