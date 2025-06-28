using Application.Features.Trips.Constants;
using Application.Features.Trips.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Shared.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Trips.Constants.TripsOperationClaims;

namespace Application.Features.Trips.Queries.GetById;

public class GetByIdTripQuery : IRequest<GetByIdTripResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdTripQueryHandler : IRequestHandler<GetByIdTripQuery, GetByIdTripResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITripRepository _tripRepository;
        private readonly TripBusinessRules _tripBusinessRules;

        public GetByIdTripQueryHandler(IMapper mapper, ITripRepository tripRepository, TripBusinessRules tripBusinessRules)
        {
            _mapper = mapper;
            _tripRepository = tripRepository;
            _tripBusinessRules = tripBusinessRules;
        }

        public async Task<GetByIdTripResponse> Handle(GetByIdTripQuery request, CancellationToken cancellationToken)
        {
            Trip? trip = await _tripRepository.GetAsync(predicate: t => t.Id == request.Id, cancellationToken: cancellationToken);
            await _tripBusinessRules.TripShouldExistWhenSelected(trip);

            GetByIdTripResponse response = _mapper.Map<GetByIdTripResponse>(trip);
            return response;
        }
    }
}