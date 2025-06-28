using Application.Features.StopTimes.Constants;
using Application.Features.StopTimes.Constants;
using Application.Features.StopTimes.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Shared.Application.Pipelines.Authorization;
using Shared.Application.Pipelines.Caching;
using Shared.Application.Pipelines.Logging;
using Shared.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.StopTimes.Constants.StopTimesOperationClaims;

namespace Application.Features.StopTimes.Commands.Delete;

public class DeleteStopTimeCommand : IRequest<DeletedStopTimeResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, StopTimesOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetStopTimes"];

    public class DeleteStopTimeCommandHandler : IRequestHandler<DeleteStopTimeCommand, DeletedStopTimeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStopTimeRepository _stopTimeRepository;
        private readonly StopTimeBusinessRules _stopTimeBusinessRules;

        public DeleteStopTimeCommandHandler(IMapper mapper, IStopTimeRepository stopTimeRepository,
                                         StopTimeBusinessRules stopTimeBusinessRules)
        {
            _mapper = mapper;
            _stopTimeRepository = stopTimeRepository;
            _stopTimeBusinessRules = stopTimeBusinessRules;
        }

        public async Task<DeletedStopTimeResponse> Handle(DeleteStopTimeCommand request, CancellationToken cancellationToken)
        {
            StopTime? stopTime = await _stopTimeRepository.GetAsync(predicate: st => st.Id == request.Id, cancellationToken: cancellationToken);
            await _stopTimeBusinessRules.StopTimeShouldExistWhenSelected(stopTime);

            await _stopTimeRepository.DeleteAsync(stopTime!);

            DeletedStopTimeResponse response = _mapper.Map<DeletedStopTimeResponse>(stopTime);
            return response;
        }
    }
}