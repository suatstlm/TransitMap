using Application.Features.Stops.Constants;
using Application.Features.Stops.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Stops.Constants.StopsOperationClaims;

namespace Application.Features.Stops.Commands.Create;

public class CreateStopCommand : IRequest<CreatedStopResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public required string StopName { get; set; }
    public required double StopLat { get; set; }
    public required double StopLon { get; set; }

    public string[] Roles => [Admin, Write, StopsOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetStops"];

    public class CreateStopCommandHandler : IRequestHandler<CreateStopCommand, CreatedStopResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStopRepository _stopRepository;
        private readonly StopBusinessRules _stopBusinessRules;

        public CreateStopCommandHandler(IMapper mapper, IStopRepository stopRepository,
                                         StopBusinessRules stopBusinessRules)
        {
            _mapper = mapper;
            _stopRepository = stopRepository;
            _stopBusinessRules = stopBusinessRules;
        }

        public async Task<CreatedStopResponse> Handle(CreateStopCommand request, CancellationToken cancellationToken)
        {
            Stop stop = _mapper.Map<Stop>(request);

            await _stopRepository.AddAsync(stop);

            CreatedStopResponse response = _mapper.Map<CreatedStopResponse>(stop);
            return response;
        }
    }
}