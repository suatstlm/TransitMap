using Application.Features.Stops.Constants;
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

namespace Application.Features.Stops.Commands.Delete;

public class DeleteStopCommand : IRequest<DeletedStopResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, StopsOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetStops"];

    public class DeleteStopCommandHandler : IRequestHandler<DeleteStopCommand, DeletedStopResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStopRepository _stopRepository;
        private readonly StopBusinessRules _stopBusinessRules;

        public DeleteStopCommandHandler(IMapper mapper, IStopRepository stopRepository,
                                         StopBusinessRules stopBusinessRules)
        {
            _mapper = mapper;
            _stopRepository = stopRepository;
            _stopBusinessRules = stopBusinessRules;
        }

        public async Task<DeletedStopResponse> Handle(DeleteStopCommand request, CancellationToken cancellationToken)
        {
            Stop? stop = await _stopRepository.GetAsync(predicate: s => s.Id == request.Id, cancellationToken: cancellationToken);
            await _stopBusinessRules.StopShouldExistWhenSelected(stop);

            await _stopRepository.DeleteAsync(stop!);

            DeletedStopResponse response = _mapper.Map<DeletedStopResponse>(stop);
            return response;
        }
    }
}