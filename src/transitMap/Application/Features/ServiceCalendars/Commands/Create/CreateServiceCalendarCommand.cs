using Application.Features.ServiceCalendars.Constants;
using Application.Features.ServiceCalendars.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Shared.Application.Pipelines.Authorization;
using Shared.Application.Pipelines.Caching;
using Shared.Application.Pipelines.Logging;
using Shared.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.ServiceCalendars.Constants.ServiceCalendarsOperationClaims;

namespace Application.Features.ServiceCalendars.Commands.Create;

public class CreateServiceCalendarCommand : IRequest<CreatedServiceCalendarResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public required bool Monday { get; set; }
    public required bool Tuesday { get; set; }
    public required bool Wednesday { get; set; }
    public required bool Thursday { get; set; }
    public required bool Friday { get; set; }
    public required bool Saturday { get; set; }
    public required bool Sunday { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }

    public string[] Roles => [Admin, Write, ServiceCalendarsOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetServiceCalendars"];

    public class CreateServiceCalendarCommandHandler : IRequestHandler<CreateServiceCalendarCommand, CreatedServiceCalendarResponse>
    {
        private readonly IMapper _mapper;
        private readonly IServiceCalendarRepository _serviceCalendarRepository;
        private readonly ServiceCalendarBusinessRules _serviceCalendarBusinessRules;

        public CreateServiceCalendarCommandHandler(IMapper mapper, IServiceCalendarRepository serviceCalendarRepository,
                                         ServiceCalendarBusinessRules serviceCalendarBusinessRules)
        {
            _mapper = mapper;
            _serviceCalendarRepository = serviceCalendarRepository;
            _serviceCalendarBusinessRules = serviceCalendarBusinessRules;
        }

        public async Task<CreatedServiceCalendarResponse> Handle(CreateServiceCalendarCommand request, CancellationToken cancellationToken)
        {
            ServiceCalendar serviceCalendar = _mapper.Map<ServiceCalendar>(request);

            await _serviceCalendarRepository.AddAsync(serviceCalendar);

            CreatedServiceCalendarResponse response = _mapper.Map<CreatedServiceCalendarResponse>(serviceCalendar);
            return response;
        }
    }
}