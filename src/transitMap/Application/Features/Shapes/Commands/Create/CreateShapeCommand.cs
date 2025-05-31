using Application.Features.Shapes.Constants;
using Application.Features.Shapes.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Shapes.Constants.ShapesOperationClaims;

namespace Application.Features.Shapes.Commands.Create;

public class CreateShapeCommand : IRequest<CreatedShapeResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public required Guid TripId { get; set; }
    public required double ShapeLat { get; set; }
    public required double ShapeLon { get; set; }
    public required int ShapePtSequence { get; set; }
    public required Trip Trip { get; set; }

    public string[] Roles => [Admin, Write, ShapesOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetShapes"];

    public class CreateShapeCommandHandler : IRequestHandler<CreateShapeCommand, CreatedShapeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IShapeRepository _shapeRepository;
        private readonly ShapeBusinessRules _shapeBusinessRules;

        public CreateShapeCommandHandler(IMapper mapper, IShapeRepository shapeRepository,
                                         ShapeBusinessRules shapeBusinessRules)
        {
            _mapper = mapper;
            _shapeRepository = shapeRepository;
            _shapeBusinessRules = shapeBusinessRules;
        }

        public async Task<CreatedShapeResponse> Handle(CreateShapeCommand request, CancellationToken cancellationToken)
        {
            Shape shape = _mapper.Map<Shape>(request);

            await _shapeRepository.AddAsync(shape);

            CreatedShapeResponse response = _mapper.Map<CreatedShapeResponse>(shape);
            return response;
        }
    }
}