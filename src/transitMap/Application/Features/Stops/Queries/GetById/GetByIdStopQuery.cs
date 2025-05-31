using Application.Features.Stops.Constants;
using Application.Features.Stops.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Stops.Constants.StopsOperationClaims;

namespace Application.Features.Stops.Queries.GetById;

public class GetByIdStopQuery : IRequest<GetByIdStopResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdStopQueryHandler : IRequestHandler<GetByIdStopQuery, GetByIdStopResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStopRepository _stopRepository;
        private readonly StopBusinessRules _stopBusinessRules;

        public GetByIdStopQueryHandler(IMapper mapper, IStopRepository stopRepository, StopBusinessRules stopBusinessRules)
        {
            _mapper = mapper;
            _stopRepository = stopRepository;
            _stopBusinessRules = stopBusinessRules;
        }

        public async Task<GetByIdStopResponse> Handle(GetByIdStopQuery request, CancellationToken cancellationToken)
        {
            Stop? stop = await _stopRepository.GetAsync(predicate: s => s.Id == request.Id, cancellationToken: cancellationToken);
            await _stopBusinessRules.StopShouldExistWhenSelected(stop);

            GetByIdStopResponse response = _mapper.Map<GetByIdStopResponse>(stop);
            return response;
        }
    }
}