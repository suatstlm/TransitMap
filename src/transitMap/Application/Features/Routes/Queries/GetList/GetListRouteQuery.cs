using Application.Features.Routes.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.Routes.Constants.RoutesOperationClaims;

namespace Application.Features.Routes.Queries.GetList;

public class GetListRouteQuery : IRequest<GetListResponse<GetListRouteListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListRoutes({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetRoutes";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListRouteQueryHandler : IRequestHandler<GetListRouteQuery, GetListResponse<GetListRouteListItemDto>>
    {
        private readonly IRouteRepository _routeRepository;
        private readonly IMapper _mapper;

        public GetListRouteQueryHandler(IRouteRepository routeRepository, IMapper mapper)
        {
            _routeRepository = routeRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListRouteListItemDto>> Handle(GetListRouteQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Route> routes = await _routeRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListRouteListItemDto> response = _mapper.Map<GetListResponse<GetListRouteListItemDto>>(routes);
            return response;
        }
    }
}