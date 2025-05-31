using Application.Features.Trips.Constants;
using Application.Features.Trips.Constants;
using Application.Features.Trips.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Trips.Constants.TripsOperationClaims;

namespace Application.Features.Trips.Commands.Delete;

public class DeleteTripCommand : IRequest<DeletedTripResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, TripsOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetTrips"];

    public class DeleteTripCommandHandler : IRequestHandler<DeleteTripCommand, DeletedTripResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITripRepository _tripRepository;
        private readonly TripBusinessRules _tripBusinessRules;

        public DeleteTripCommandHandler(IMapper mapper, ITripRepository tripRepository,
                                         TripBusinessRules tripBusinessRules)
        {
            _mapper = mapper;
            _tripRepository = tripRepository;
            _tripBusinessRules = tripBusinessRules;
        }

        public async Task<DeletedTripResponse> Handle(DeleteTripCommand request, CancellationToken cancellationToken)
        {
            Trip? trip = await _tripRepository.GetAsync(predicate: t => t.Id == request.Id, cancellationToken: cancellationToken);
            await _tripBusinessRules.TripShouldExistWhenSelected(trip);

            await _tripRepository.DeleteAsync(trip!);

            DeletedTripResponse response = _mapper.Map<DeletedTripResponse>(trip);
            return response;
        }
    }
}