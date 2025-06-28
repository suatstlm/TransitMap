using Application.Features.Agencies.Constants;
using Application.Features.Agencies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Shared.Application.Pipelines.Authorization;
using Shared.Application.Pipelines.Caching;
using Shared.Application.Pipelines.Logging;
using Shared.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Agencies.Constants.AgenciesOperationClaims;

namespace Application.Features.Agencies.Commands.Update;

public class UpdateAgencyCommand : IRequest<UpdatedAgencyResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public required string AgencyName { get; set; }
    public required string AgencyUrl { get; set; }
    public required string AgencyTimezone { get; set; }
    public required string AgencyLang { get; set; }
    public required string AgencyPhone { get; set; }

    public string[] Roles => [Admin, Write, AgenciesOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetAgencies"];

    public class UpdateAgencyCommandHandler : IRequestHandler<UpdateAgencyCommand, UpdatedAgencyResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAgencyRepository _agencyRepository;
        private readonly AgencyBusinessRules _agencyBusinessRules;

        public UpdateAgencyCommandHandler(IMapper mapper, IAgencyRepository agencyRepository,
                                         AgencyBusinessRules agencyBusinessRules)
        {
            _mapper = mapper;
            _agencyRepository = agencyRepository;
            _agencyBusinessRules = agencyBusinessRules;
        }

        public async Task<UpdatedAgencyResponse> Handle(UpdateAgencyCommand request, CancellationToken cancellationToken)
        {
            Agency? agency = await _agencyRepository.GetAsync(predicate: a => a.Id == request.Id, cancellationToken: cancellationToken);
            await _agencyBusinessRules.AgencyShouldExistWhenSelected(agency);
            agency = _mapper.Map(request, agency);

            await _agencyRepository.UpdateAsync(agency!);

            UpdatedAgencyResponse response = _mapper.Map<UpdatedAgencyResponse>(agency);
            return response;
        }
    }
}