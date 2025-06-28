using Application.Features.Stops.Constants;
using Application.Features.Stops.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Shared.Application.Pipelines.Authorization;
using Shared.Application.Pipelines.Caching;
using Shared.Application.Pipelines.Logging;
using Shared.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Stops.Constants.StopsOperationClaims;

namespace Application.Features.Stops.Commands.Update;

public class UpdateStopCommand : IRequest<UpdatedStopResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public required string StopName { get; set; }
    public required double StopLat { get; set; }
    public required double StopLon { get; set; }

    public string[] Roles => [Admin, Write, StopsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetStops"];

    public class UpdateStopCommandHandler : IRequestHandler<UpdateStopCommand, UpdatedStopResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStopRepository _stopRepository;
        private readonly StopBusinessRules _stopBusinessRules;

        public UpdateStopCommandHandler(IMapper mapper, IStopRepository stopRepository,
                                         StopBusinessRules stopBusinessRules)
        {
            _mapper = mapper;
            _stopRepository = stopRepository;
            _stopBusinessRules = stopBusinessRules;
        }

        public async Task<UpdatedStopResponse> Handle(UpdateStopCommand request, CancellationToken cancellationToken)
        {
            Stop? stop = await _stopRepository.GetAsync(predicate: s => s.Id == request.Id, cancellationToken: cancellationToken);
            await _stopBusinessRules.StopShouldExistWhenSelected(stop);
            stop = _mapper.Map(request, stop);

            await _stopRepository.UpdateAsync(stop!);

            UpdatedStopResponse response = _mapper.Map<UpdatedStopResponse>(stop);
            return response;
        }
    }
}