using Application.Features.Agencies.Constants;
using Application.Features.Agencies.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Agencies.Constants.AgenciesOperationClaims;

namespace Application.Features.Agencies.Queries.GetById;

public class GetByIdAgencyQuery : IRequest<GetByIdAgencyResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdAgencyQueryHandler : IRequestHandler<GetByIdAgencyQuery, GetByIdAgencyResponse>
    {
        private readonly IMapper _mapper;
        private readonly IAgencyRepository _agencyRepository;
        private readonly AgencyBusinessRules _agencyBusinessRules;

        public GetByIdAgencyQueryHandler(IMapper mapper, IAgencyRepository agencyRepository, AgencyBusinessRules agencyBusinessRules)
        {
            _mapper = mapper;
            _agencyRepository = agencyRepository;
            _agencyBusinessRules = agencyBusinessRules;
        }

        public async Task<GetByIdAgencyResponse> Handle(GetByIdAgencyQuery request, CancellationToken cancellationToken)
        {
            Agency? agency = await _agencyRepository.GetAsync(predicate: a => a.Id == request.Id, cancellationToken: cancellationToken);
            await _agencyBusinessRules.AgencyShouldExistWhenSelected(agency);

            GetByIdAgencyResponse response = _mapper.Map<GetByIdAgencyResponse>(agency);
            return response;
        }
    }
}