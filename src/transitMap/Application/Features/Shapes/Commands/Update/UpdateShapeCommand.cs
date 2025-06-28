using Application.Features.Shapes.Constants;
using Application.Features.Shapes.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Shared.Application.Pipelines.Authorization;
using Shared.Application.Pipelines.Caching;
using Shared.Application.Pipelines.Logging;
using Shared.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Shapes.Constants.ShapesOperationClaims;

namespace Application.Features.Shapes.Commands.Update;

public class UpdateShapeCommand : IRequest<UpdatedShapeResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public required Guid TripId { get; set; }
    public required double ShapeLat { get; set; }
    public required double ShapeLon { get; set; }
    public required int ShapePtSequence { get; set; }
    public required Trip Trip { get; set; }

    public string[] Roles => [Admin, Write, ShapesOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetShapes"];

    public class UpdateShapeCommandHandler : IRequestHandler<UpdateShapeCommand, UpdatedShapeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IShapeRepository _shapeRepository;
        private readonly ShapeBusinessRules _shapeBusinessRules;

        public UpdateShapeCommandHandler(IMapper mapper, IShapeRepository shapeRepository,
                                         ShapeBusinessRules shapeBusinessRules)
        {
            _mapper = mapper;
            _shapeRepository = shapeRepository;
            _shapeBusinessRules = shapeBusinessRules;
        }

        public async Task<UpdatedShapeResponse> Handle(UpdateShapeCommand request, CancellationToken cancellationToken)
        {
            Shape? shape = await _shapeRepository.GetAsync(predicate: s => s.Id == request.Id, cancellationToken: cancellationToken);
            await _shapeBusinessRules.ShapeShouldExistWhenSelected(shape);
            shape = _mapper.Map(request, shape);

            await _shapeRepository.UpdateAsync(shape!);

            UpdatedShapeResponse response = _mapper.Map<UpdatedShapeResponse>(shape);
            return response;
        }
    }
}