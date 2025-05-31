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

namespace Application.Features.Trips.Commands.Update;

public class UpdateTripCommand : IRequest<UpdatedTripResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public required Guid RouteId { get; set; }
    public required Guid ServiceId { get; set; }
    public required string TripHeadsign { get; set; }
    public required Route Route { get; set; }
    public required ServiceCalendar ServiceCalendar { get; set; }

    public string[] Roles => [Admin, Write, TripsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetTrips"];

    public class UpdateTripCommandHandler : IRequestHandler<UpdateTripCommand, UpdatedTripResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITripRepository _tripRepository;
        private readonly TripBusinessRules _tripBusinessRules;

        public UpdateTripCommandHandler(IMapper mapper, ITripRepository tripRepository,
                                         TripBusinessRules tripBusinessRules)
        {
            _mapper = mapper;
            _tripRepository = tripRepository;
            _tripBusinessRules = tripBusinessRules;
        }

        public async Task<UpdatedTripResponse> Handle(UpdateTripCommand request, CancellationToken cancellationToken)
        {
            Trip? trip = await _tripRepository.GetAsync(predicate: t => t.Id == request.Id, cancellationToken: cancellationToken);
            await _tripBusinessRules.TripShouldExistWhenSelected(trip);
            trip = _mapper.Map(request, trip);

            await _tripRepository.UpdateAsync(trip!);

            UpdatedTripResponse response = _mapper.Map<UpdatedTripResponse>(trip);
            return response;
        }
    }
}