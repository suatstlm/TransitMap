using Shared.Application.Responses;

namespace Application.Features.Agencies.Queries.GetById;

public class GetByIdAgencyResponse : IResponse
{
    public Guid Id { get; set; }
    public string AgencyName { get; set; }
    public string AgencyUrl { get; set; }
    public string AgencyTimezone { get; set; }
    public string AgencyLang { get; set; }
    public string AgencyPhone { get; set; }
}