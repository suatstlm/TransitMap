using Application.Features.ServiceCalendars.Constants;
using Application.Features.ServiceCalendars.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.ServiceCalendars.Constants.ServiceCalendarsOperationClaims;

namespace Application.Features.ServiceCalendars.Commands.Update;

public class UpdateServiceCalendarCommand : IRequest<UpdatedServiceCalendarResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public required bool Monday { get; set; }
    public required bool Tuesday { get; set; }
    public required bool Wednesday { get; set; }
    public required bool Thursday { get; set; }
    public required bool Friday { get; set; }
    public required bool Saturday { get; set; }
    public required bool Sunday { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }

    public string[] Roles => [Admin, Write, ServiceCalendarsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetServiceCalendars"];

    public class UpdateServiceCalendarCommandHandler : IRequestHandler<UpdateServiceCalendarCommand, UpdatedServiceCalendarResponse>
    {
        private readonly IMapper _mapper;
        private readonly IServiceCalendarRepository _serviceCalendarRepository;
        private readonly ServiceCalendarBusinessRules _serviceCalendarBusinessRules;

        public UpdateServiceCalendarCommandHandler(IMapper mapper, IServiceCalendarRepository serviceCalendarRepository,
                                         ServiceCalendarBusinessRules serviceCalendarBusinessRules)
        {
            _mapper = mapper;
            _serviceCalendarRepository = serviceCalendarRepository;
            _serviceCalendarBusinessRules = serviceCalendarBusinessRules;
        }

        public async Task<UpdatedServiceCalendarResponse> Handle(UpdateServiceCalendarCommand request, CancellationToken cancellationToken)
        {
            ServiceCalendar? serviceCalendar = await _serviceCalendarRepository.GetAsync(predicate: sc => sc.Id == request.Id, cancellationToken: cancellationToken);
            await _serviceCalendarBusinessRules.ServiceCalendarShouldExistWhenSelected(serviceCalendar);
            serviceCalendar = _mapper.Map(request, serviceCalendar);

            await _serviceCalendarRepository.UpdateAsync(serviceCalendar!);

            UpdatedServiceCalendarResponse response = _mapper.Map<UpdatedServiceCalendarResponse>(serviceCalendar);
            return response;
        }
    }
}