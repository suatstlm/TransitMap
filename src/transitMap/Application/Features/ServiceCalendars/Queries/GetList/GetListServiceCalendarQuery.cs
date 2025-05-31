using Application.Features.ServiceCalendars.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.ServiceCalendars.Constants.ServiceCalendarsOperationClaims;

namespace Application.Features.ServiceCalendars.Queries.GetList;

public class GetListServiceCalendarQuery : IRequest<GetListResponse<GetListServiceCalendarListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListServiceCalendars({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetServiceCalendars";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListServiceCalendarQueryHandler : IRequestHandler<GetListServiceCalendarQuery, GetListResponse<GetListServiceCalendarListItemDto>>
    {
        private readonly IServiceCalendarRepository _serviceCalendarRepository;
        private readonly IMapper _mapper;

        public GetListServiceCalendarQueryHandler(IServiceCalendarRepository serviceCalendarRepository, IMapper mapper)
        {
            _serviceCalendarRepository = serviceCalendarRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListServiceCalendarListItemDto>> Handle(GetListServiceCalendarQuery request, CancellationToken cancellationToken)
        {
            IPaginate<ServiceCalendar> serviceCalendars = await _serviceCalendarRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListServiceCalendarListItemDto> response = _mapper.Map<GetListResponse<GetListServiceCalendarListItemDto>>(serviceCalendars);
            return response;
        }
    }
}