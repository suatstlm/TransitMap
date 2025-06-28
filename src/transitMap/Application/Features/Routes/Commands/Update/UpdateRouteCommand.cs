using Application.Features.Routes.Constants;
using Application.Features.Routes.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Shared.Application.Pipelines.Authorization;
using Shared.Application.Pipelines.Caching;
using Shared.Application.Pipelines.Logging;
using Shared.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Routes.Constants.RoutesOperationClaims;

namespace Application.Features.Routes.Commands.Update;

public class UpdateRouteCommand : IRequest<UpdatedRouteResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public required Guid AgencyId { get; set; }
    public required string RouteShortName { get; set; }
    public required string RouteLongName { get; set; }
    public required int RouteType { get; set; }
    public required Agency Agency { get; set; }

    public string[] Roles => [Admin, Write, RoutesOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetRoutes"];

    public class UpdateRouteCommandHandler : IRequestHandler<UpdateRouteCommand, UpdatedRouteResponse>
    {
        private readonly IMapper _mapper;
        private readonly IRouteRepository _routeRepository;
        private readonly RouteBusinessRules _routeBusinessRules;

        public UpdateRouteCommandHandler(IMapper mapper, IRouteRepository routeRepository,
                                         RouteBusinessRules routeBusinessRules)
        {
            _mapper = mapper;
            _routeRepository = routeRepository;
            _routeBusinessRules = routeBusinessRules;
        }

        public async Task<UpdatedRouteResponse> Handle(UpdateRouteCommand request, CancellationToken cancellationToken)
        {
            Route? route = await _routeRepository.GetAsync(predicate: r => r.Id == request.Id, cancellationToken: cancellationToken);
            await _routeBusinessRules.RouteShouldExistWhenSelected(route);
            route = _mapper.Map(request, route);

            await _routeRepository.UpdateAsync(route!);

            UpdatedRouteResponse response = _mapper.Map<UpdatedRouteResponse>(route);
            return response;
        }
    }
}