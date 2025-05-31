using Application.Features.Routes.Constants;
using Application.Features.Routes.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Routes.Constants.RoutesOperationClaims;

namespace Application.Features.Routes.Commands.Create;

public class CreateRouteCommand : IRequest<CreatedRouteResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public required Guid AgencyId { get; set; }
    public required string RouteShortName { get; set; }
    public required string RouteLongName { get; set; }
    public required int RouteType { get; set; }
    public required Agency Agency { get; set; }

    public string[] Roles => [Admin, Write, RoutesOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetRoutes"];

    public class CreateRouteCommandHandler : IRequestHandler<CreateRouteCommand, CreatedRouteResponse>
    {
        private readonly IMapper _mapper;
        private readonly IRouteRepository _routeRepository;
        private readonly RouteBusinessRules _routeBusinessRules;

        public CreateRouteCommandHandler(IMapper mapper, IRouteRepository routeRepository,
                                         RouteBusinessRules routeBusinessRules)
        {
            _mapper = mapper;
            _routeRepository = routeRepository;
            _routeBusinessRules = routeBusinessRules;
        }

        public async Task<CreatedRouteResponse> Handle(CreateRouteCommand request, CancellationToken cancellationToken)
        {
            Route route = _mapper.Map<Route>(request);

            await _routeRepository.AddAsync(route);

            CreatedRouteResponse response = _mapper.Map<CreatedRouteResponse>(route);
            return response;
        }
    }
}