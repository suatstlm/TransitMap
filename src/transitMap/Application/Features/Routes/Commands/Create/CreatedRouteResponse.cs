using Domain.Entities;
using Shared.Application.Responses;

namespace Application.Features.Routes.Commands.Create;

public class CreatedRouteResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid AgencyId { get; set; }
    public string RouteShortName { get; set; }
    public string RouteLongName { get; set; }
    public int RouteType { get; set; }
    public Agency Agency { get; set; }
}