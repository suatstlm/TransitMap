using Shared.Application.Responses;

namespace Application.Features.Routes.Commands.Delete;

public class DeletedRouteResponse : IResponse
{
    public Guid Id { get; set; }
}