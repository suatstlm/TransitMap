using Application.Features.Trips.Constants;
using Application.Features.Trips.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Shared.Application.Pipelines.Authorization;
using Shared.Application.Pipelines.Caching;
using Shared.Application.Pipelines.Logging;
using Shared.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Trips.Constants.TripsOperationClaims;

namespace Application.Features.Trips.Commands.Create;

public class CreateTripCommand : IRequest<CreatedTripResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public required Guid RouteId { get; set; }
    public required Guid ServiceId { get; set; }
    public required string TripHeadsign { get; set; }
    public required Route Route { get; set; }
    public required ServiceCalendar ServiceCalendar { get; set; }

    public string[] Roles => [Admin, Write, TripsOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetTrips"];

    public class CreateTripCommandHandler : IRequestHandler<CreateTripCommand, CreatedTripResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITripRepository _tripRepository;
        private readonly TripBusinessRules _tripBusinessRules;

        public CreateTripCommandHandler(IMapper mapper, ITripRepository tripRepository,
                                         TripBusinessRules tripBusinessRules)
        {
            _mapper = mapper;
            _tripRepository = tripRepository;
            _tripBusinessRules = tripBusinessRules;
        }

        public async Task<CreatedTripResponse> Handle(CreateTripCommand request, CancellationToken cancellationToken)
        {
            Trip trip = _mapper.Map<Trip>(request);

            await _tripRepository.AddAsync(trip);

            CreatedTripResponse response = _mapper.Map<CreatedTripResponse>(trip);
            return response;
        }
    }
}