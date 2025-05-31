using Domain.Entities;
using NArchitecture.Core.Application.Responses;

namespace Application.Features.Routes.Queries.GetById;

public class GetByIdRouteResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid AgencyId { get; set; }
    public string RouteShortName { get; set; }
    public string RouteLongName { get; set; }
    public int RouteType { get; set; }
    public Agency Agency { get; set; }
}