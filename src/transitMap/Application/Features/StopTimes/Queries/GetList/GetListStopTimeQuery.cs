using Application.Features.StopTimes.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.StopTimes.Constants.StopTimesOperationClaims;

namespace Application.Features.StopTimes.Queries.GetList;

public class GetListStopTimeQuery : IRequest<GetListResponse<GetListStopTimeListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListStopTimes({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetStopTimes";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListStopTimeQueryHandler : IRequestHandler<GetListStopTimeQuery, GetListResponse<GetListStopTimeListItemDto>>
    {
        private readonly IStopTimeRepository _stopTimeRepository;
        private readonly IMapper _mapper;

        public GetListStopTimeQueryHandler(IStopTimeRepository stopTimeRepository, IMapper mapper)
        {
            _stopTimeRepository = stopTimeRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListStopTimeListItemDto>> Handle(GetListStopTimeQuery request, CancellationToken cancellationToken)
        {
            IPaginate<StopTime> stopTimes = await _stopTimeRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListStopTimeListItemDto> response = _mapper.Map<GetListResponse<GetListStopTimeListItemDto>>(stopTimes);
            return response;
        }
    }
}