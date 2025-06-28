using Domain.Entities;
using Shared.Application.Dtos;

namespace Application.Features.Routes.Queries.GetList;

public class GetListRouteListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid AgencyId { get; set; }
    public string RouteShortName { get; set; }
    public string RouteLongName { get; set; }
    public int RouteType { get; set; }
    public Agency Agency { get; set; }
}