using Application.Features.Agencies.Constants;
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

namespace Application.Features.Agencies.Commands.Delete;

public class DeleteAgencyCommand : IRequest<DeletedAgencyResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, AgenciesOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetAgencies"];

    public class DeleteAgencyCommandHandler : IRequestHandler<DeleteAgencyCommand, DeletedAgencyResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAgencyRepository _agencyRepository;
        private readonly AgencyBusinessRules _agencyBusinessRules;

        public DeleteAgencyCommandHandler(IMapper mapper, IAgencyRepository agencyRepository,
                                         AgencyBusinessRules agencyBusinessRules)
        {
            _mapper = mapper;
            _agencyRepository = agencyRepository;
            _agencyBusinessRules = agencyBusinessRules;
        }

        public async Task<DeletedAgencyResponse> Handle(DeleteAgencyCommand request, CancellationToken cancellationToken)
        {
            Agency? agency = await _agencyRepository.GetAsync(predicate: a => a.Id == request.Id, cancellationToken: cancellationToken);
            await _agencyBusinessRules.AgencyShouldExistWhenSelected(agency);

            await _agencyRepository.DeleteAsync(agency!);

            DeletedAgencyResponse response = _mapper.Map<DeletedAgencyResponse>(agency);
            return response;
        }
    }
}