using Application.Features.ServiceCalendars.Constants;
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

namespace Application.Features.ServiceCalendars.Commands.Delete;

public class DeleteServiceCalendarCommand : IRequest<DeletedServiceCalendarResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, ServiceCalendarsOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetServiceCalendars"];

    public class DeleteServiceCalendarCommandHandler : IRequestHandler<DeleteServiceCalendarCommand, DeletedServiceCalendarResponse>
    {
        private readonly IMapper _mapper;
        private readonly IServiceCalendarRepository _serviceCalendarRepository;
        private readonly ServiceCalendarBusinessRules _serviceCalendarBusinessRules;

        public DeleteServiceCalendarCommandHandler(IMapper mapper, IServiceCalendarRepository serviceCalendarRepository,
                                         ServiceCalendarBusinessRules serviceCalendarBusinessRules)
        {
            _mapper = mapper;
            _serviceCalendarRepository = serviceCalendarRepository;
            _serviceCalendarBusinessRules = serviceCalendarBusinessRules;
        }

        public async Task<DeletedServiceCalendarResponse> Handle(DeleteServiceCalendarCommand request, CancellationToken cancellationToken)
        {
            ServiceCalendar? serviceCalendar = await _serviceCalendarRepository.GetAsync(predicate: sc => sc.Id == request.Id, cancellationToken: cancellationToken);
            await _serviceCalendarBusinessRules.ServiceCalendarShouldExistWhenSelected(serviceCalendar);

            await _serviceCalendarRepository.DeleteAsync(serviceCalendar!);

            DeletedServiceCalendarResponse response = _mapper.Map<DeletedServiceCalendarResponse>(serviceCalendar);
            return response;
        }
    }
}