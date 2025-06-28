using Application.Features.Trips.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Shared.Application.Pipelines.Authorization;
using Shared.Application.Pipelines.Caching;
using Shared.Application.Requests;
using Shared.Application.Responses;
using Shared.Persistence.Paging;
using MediatR;
using static Application.Features.Trips.Constants.TripsOperationClaims;

namespace Application.Features.Trips.Queries.GetList;

public class GetListTripQuery : IRequest<GetListResponse<GetListTripListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListTrips({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetTrips";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListTripQueryHandler : IRequestHandler<GetListTripQuery, GetListResponse<GetListTripListItemDto>>
    {
        private readonly ITripRepository _tripRepository;
        private readonly IMapper _mapper;

        public GetListTripQueryHandler(ITripRepository tripRepository, IMapper mapper)
        {
            _tripRepository = tripRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListTripListItemDto>> Handle(GetListTripQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Trip> trips = await _tripRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListTripListItemDto> response = _mapper.Map<GetListResponse<GetListTripListItemDto>>(trips);
            return response;
        }
    }
}