using Application.Features.Agencies.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.Agencies.Constants.AgenciesOperationClaims;

namespace Application.Features.Agencies.Queries.GetList;

public class GetListAgencyQuery : IRequest<GetListResponse<GetListAgencyListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListAgencies({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetAgencies";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListAgencyQueryHandler : IRequestHandler<GetListAgencyQuery, GetListResponse<GetListAgencyListItemDto>>
    {
        private readonly IAgencyRepository _agencyRepository;
        private readonly IMapper _mapper;

        public GetListAgencyQueryHandler(IAgencyRepository agencyRepository, IMapper mapper)
        {
            _agencyRepository = agencyRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListAgencyListItemDto>> Handle(GetListAgencyQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Agency> agencies = await _agencyRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListAgencyListItemDto> response = _mapper.Map<GetListResponse<GetListAgencyListItemDto>>(agencies);
            return response;
        }
    }
}