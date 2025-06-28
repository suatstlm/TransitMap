using Application.Features.ServiceCalendars.Constants;
using Application.Features.ServiceCalendars.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Shared.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.ServiceCalendars.Constants.ServiceCalendarsOperationClaims;

namespace Application.Features.ServiceCalendars.Queries.GetById;

public class GetByIdServiceCalendarQuery : IRequest<GetByIdServiceCalendarResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdServiceCalendarQueryHandler : IRequestHandler<GetByIdServiceCalendarQuery, GetByIdServiceCalendarResponse>
    {
        private readonly IMapper _mapper;
        private readonly IServiceCalendarRepository _serviceCalendarRepository;
        private readonly ServiceCalendarBusinessRules _serviceCalendarBusinessRules;

        public GetByIdServiceCalendarQueryHandler(IMapper mapper, IServiceCalendarRepository serviceCalendarRepository, ServiceCalendarBusinessRules serviceCalendarBusinessRules)
        {
            _mapper = mapper;
            _serviceCalendarRepository = serviceCalendarRepository;
            _serviceCalendarBusinessRules = serviceCalendarBusinessRules;
        }

        public async Task<GetByIdServiceCalendarResponse> Handle(GetByIdServiceCalendarQuery request, CancellationToken cancellationToken)
        {
            ServiceCalendar? serviceCalendar = await _serviceCalendarRepository.GetAsync(predicate: sc => sc.Id == request.Id, cancellationToken: cancellationToken);
            await _serviceCalendarBusinessRules.ServiceCalendarShouldExistWhenSelected(serviceCalendar);

            GetByIdServiceCalendarResponse response = _mapper.Map<GetByIdServiceCalendarResponse>(serviceCalendar);
            return response;
        }
    }
}