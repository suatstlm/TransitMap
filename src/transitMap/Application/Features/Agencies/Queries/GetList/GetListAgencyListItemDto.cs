using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Agencies.Queries.GetList;

public class GetListAgencyListItemDto : IDto
{
    public Guid Id { get; set; }
    public string AgencyName { get; set; }
    public string AgencyUrl { get; set; }
    public string AgencyTimezone { get; set; }
    public string AgencyLang { get; set; }
    public string AgencyPhone { get; set; }
}