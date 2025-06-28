using Application.Features.StopTimes.Constants;
using Application.Features.StopTimes.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Shared.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.StopTimes.Constants.StopTimesOperationClaims;

namespace Application.Features.StopTimes.Queries.GetById;

public class GetByIdStopTimeQuery : IRequest<GetByIdStopTimeResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdStopTimeQueryHandler : IRequestHandler<GetByIdStopTimeQuery, GetByIdStopTimeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStopTimeRepository _stopTimeRepository;
        private readonly StopTimeBusinessRules _stopTimeBusinessRules;

        public GetByIdStopTimeQueryHandler(IMapper mapper, IStopTimeRepository stopTimeRepository, StopTimeBusinessRules stopTimeBusinessRules)
        {
            _mapper = mapper;
            _stopTimeRepository = stopTimeRepository;
            _stopTimeBusinessRules = stopTimeBusinessRules;
        }

        public async Task<GetByIdStopTimeResponse> Handle(GetByIdStopTimeQuery request, CancellationToken cancellationToken)
        {
            StopTime? stopTime = await _stopTimeRepository.GetAsync(predicate: st => st.Id == request.Id, cancellationToken: cancellationToken);
            await _stopTimeBusinessRules.StopTimeShouldExistWhenSelected(stopTime);

            GetByIdStopTimeResponse response = _mapper.Map<GetByIdStopTimeResponse>(stopTime);
            return response;
        }
    }
}