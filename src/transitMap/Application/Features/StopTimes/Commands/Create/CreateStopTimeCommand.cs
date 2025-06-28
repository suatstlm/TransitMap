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

namespace Application.Features.StopTimes.Commands.Create;

public class CreateStopTimeCommand : IRequest<CreatedStopTimeResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public required Guid TripId { get; set; }
    public required Guid StopId { get; set; }
    public required TimeSpan ArrivalTime { get; set; }
    public required TimeSpan DepartureTime { get; set; }
    public required int StopSequence { get; set; }
    public required Trip Trip { get; set; }
    public required Stop Stop { get; set; }

    public string[] Roles => [Admin, Write, StopTimesOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetStopTimes"];

    public class CreateStopTimeCommandHandler : IRequestHandler<CreateStopTimeCommand, CreatedStopTimeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStopTimeRepository _stopTimeRepository;
        private readonly StopTimeBusinessRules _stopTimeBusinessRules;

        public CreateStopTimeCommandHandler(IMapper mapper, IStopTimeRepository stopTimeRepository,
                                         StopTimeBusinessRules stopTimeBusinessRules)
        {
            _mapper = mapper;
            _stopTimeRepository = stopTimeRepository;
            _stopTimeBusinessRules = stopTimeBusinessRules;
        }

        public async Task<CreatedStopTimeResponse> Handle(CreateStopTimeCommand request, CancellationToken cancellationToken)
        {
            StopTime stopTime = _mapper.Map<StopTime>(request);

            await _stopTimeRepository.AddAsync(stopTime);

            CreatedStopTimeResponse response = _mapper.Map<CreatedStopTimeResponse>(stopTime);
            return response;
        }
    }
}