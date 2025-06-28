using Application.Features.Shapes.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Shared.Application.Pipelines.Authorization;
using Shared.Application.Pipelines.Caching;
using Shared.Application.Requests;
using Shared.Application.Responses;
using Shared.Persistence.Paging;
using MediatR;
using static Application.Features.Shapes.Constants.ShapesOperationClaims;

namespace Application.Features.Shapes.Queries.GetList;

public class GetListShapeQuery : IRequest<GetListResponse<GetListShapeListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListShapes({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetShapes";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListShapeQueryHandler : IRequestHandler<GetListShapeQuery, GetListResponse<GetListShapeListItemDto>>
    {
        private readonly IShapeRepository _shapeRepository;
        private readonly IMapper _mapper;

        public GetListShapeQueryHandler(IShapeRepository shapeRepository, IMapper mapper)
        {
            _shapeRepository = shapeRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListShapeListItemDto>> Handle(GetListShapeQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Shape> shapes = await _shapeRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListShapeListItemDto> response = _mapper.Map<GetListResponse<GetListShapeListItemDto>>(shapes);
            return response;
        }
    }
}