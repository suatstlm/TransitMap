using Application.Features.Agencies.Constants;
using Application.Features.Agencies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Agencies.Constants.AgenciesOperationClaims;

namespace Application.Features.Agencies.Commands.Create;

public class CreateAgencyCommand : IRequest<CreatedAgencyResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public required string AgencyName { get; set; }
    public required string AgencyUrl { get; set; }
    public required string AgencyTimezone { get; set; }
    public required string AgencyLang { get; set; }
    public required string AgencyPhone { get; set; }

    public string[] Roles => [Admin, Write, AgenciesOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetAgencies"];

    public class CreateAgencyCommandHandler : IRequestHandler<CreateAgencyCommand, CreatedAgencyResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAgencyRepository _agencyRepository;
        private readonly AgencyBusinessRules _agencyBusinessRules;

        public CreateAgencyCommandHandler(IMapper mapper, IAgencyRepository agencyRepository,
                                         AgencyBusinessRules agencyBusinessRules)
        {
            _mapper = mapper;
            _agencyRepository = agencyRepository;
            _agencyBusinessRules = agencyBusinessRules;
        }

        public async Task<CreatedAgencyResponse> Handle(CreateAgencyCommand request, CancellationToken cancellationToken)
        {
            Agency agency = _mapper.Map<Agency>(request);

            await _agencyRepository.AddAsync(agency);

            CreatedAgencyResponse response = _mapper.Map<CreatedAgencyResponse>(agency);
            return response;
        }
    }
}