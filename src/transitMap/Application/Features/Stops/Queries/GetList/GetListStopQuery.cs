using Application.Features.Stops.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Shared.Application.Pipelines.Authorization;
using Shared.Application.Pipelines.Caching;
using Shared.Application.Requests;
using Shared.Application.Responses;
using Shared.Persistence.Paging;
using MediatR;
using static Application.Features.Stops.Constants.StopsOperationClaims;

namespace Application.Features.Stops.Queries.GetList;

public class GetListStopQuery : IRequest<GetListResponse<GetListStopListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListStops({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetStops";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListStopQueryHandler : IRequestHandler<GetListStopQuery, GetListResponse<GetListStopListItemDto>>
    {
        private readonly IStopRepository _stopRepository;
        private readonly IMapper _mapper;

        public GetListStopQueryHandler(IStopRepository stopRepository, IMapper mapper)
        {
            _stopRepository = stopRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListStopListItemDto>> Handle(GetListStopQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Stop> stops = await _stopRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListStopListItemDto> response = _mapper.Map<GetListResponse<GetListStopListItemDto>>(stops);
            return response;
        }
    }
}