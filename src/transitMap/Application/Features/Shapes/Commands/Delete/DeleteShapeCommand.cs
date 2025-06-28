using Application.Features.Shapes.Constants;
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

namespace Application.Features.Shapes.Commands.Delete;

public class DeleteShapeCommand : IRequest<DeletedShapeResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, ShapesOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetShapes"];

    public class DeleteShapeCommandHandler : IRequestHandler<DeleteShapeCommand, DeletedShapeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IShapeRepository _shapeRepository;
        private readonly ShapeBusinessRules _shapeBusinessRules;

        public DeleteShapeCommandHandler(IMapper mapper, IShapeRepository shapeRepository,
                                         ShapeBusinessRules shapeBusinessRules)
        {
            _mapper = mapper;
            _shapeRepository = shapeRepository;
            _shapeBusinessRules = shapeBusinessRules;
        }

        public async Task<DeletedShapeResponse> Handle(DeleteShapeCommand request, CancellationToken cancellationToken)
        {
            Shape? shape = await _shapeRepository.GetAsync(predicate: s => s.Id == request.Id, cancellationToken: cancellationToken);
            await _shapeBusinessRules.ShapeShouldExistWhenSelected(shape);

            await _shapeRepository.DeleteAsync(shape!);

            DeletedShapeResponse response = _mapper.Map<DeletedShapeResponse>(shape);
            return response;
        }
    }
}