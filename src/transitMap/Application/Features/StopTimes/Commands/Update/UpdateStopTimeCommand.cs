using Application.Features.StopTimes.Constants;
using Application.Features.StopTimes.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.StopTimes.Constants.StopTimesOperationClaims;

namespace Application.Features.StopTimes.Commands.Update;

public class UpdateStopTimeCommand : IRequest<UpdatedStopTimeResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public required Guid TripId { get; set; }
    public required Guid StopId { get; set; }
    public required TimeSpan ArrivalTime { get; set; }
    public required TimeSpan DepartureTime { get; set; }
    public required int StopSequence { get; set; }
    public required Trip Trip { get; set; }
    public required Stop Stop { get; set; }

    public string[] Roles => [Admin, Write, StopTimesOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetStopTimes"];

    public class UpdateStopTimeCommandHandler : IRequestHandler<UpdateStopTimeCommand, UpdatedStopTimeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStopTimeRepository _stopTimeRepository;
        private readonly StopTimeBusinessRules _stopTimeBusinessRules;

        public UpdateStopTimeCommandHandler(IMapper mapper, IStopTimeRepository stopTimeRepository,
                                         StopTimeBusinessRules stopTimeBusinessRules)
        {
            _mapper = mapper;
            _stopTimeRepository = stopTimeRepository;
            _stopTimeBusinessRules = stopTimeBusinessRules;
        }

        public async Task<UpdatedStopTimeResponse> Handle(UpdateStopTimeCommand request, CancellationToken cancellationToken)
        {
            StopTime? stopTime = await _stopTimeRepository.GetAsync(predicate: st => st.Id == request.Id, cancellationToken: cancellationToken);
            await _stopTimeBusinessRules.StopTimeShouldExistWhenSelected(stopTime);
            stopTime = _mapper.Map(request, stopTime);

            await _stopTimeRepository.UpdateAsync(stopTime!);

            UpdatedStopTimeResponse response = _mapper.Map<UpdatedStopTimeResponse>(stopTime);
            return response;
        }
    }
}