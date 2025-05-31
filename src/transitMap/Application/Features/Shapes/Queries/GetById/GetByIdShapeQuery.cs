using Application.Features.Shapes.Constants;
using Application.Features.Shapes.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Shapes.Constants.ShapesOperationClaims;

namespace Application.Features.Shapes.Queries.GetById;

public class GetByIdShapeQuery : IRequest<GetByIdShapeResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdShapeQueryHandler : IRequestHandler<GetByIdShapeQuery, GetByIdShapeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IShapeRepository _shapeRepository;
        private readonly ShapeBusinessRules _shapeBusinessRules;

        public GetByIdShapeQueryHandler(IMapper mapper, IShapeRepository shapeRepository, ShapeBusinessRules shapeBusinessRules)
        {
            _mapper = mapper;
            _shapeRepository = shapeRepository;
            _shapeBusinessRules = shapeBusinessRules;
        }

        public async Task<GetByIdShapeResponse> Handle(GetByIdShapeQuery request, CancellationToken cancellationToken)
        {
            Shape? shape = await _shapeRepository.GetAsync(predicate: s => s.Id == request.Id, cancellationToken: cancellationToken);
            await _shapeBusinessRules.ShapeShouldExistWhenSelected(shape);

            GetByIdShapeResponse response = _mapper.Map<GetByIdShapeResponse>(shape);
            return response;
        }
    }
}