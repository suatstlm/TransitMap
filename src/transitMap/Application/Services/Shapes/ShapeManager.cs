using Application.Features.Shapes.Rules;
using Application.Services.Repositories;
using Shared.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Shapes;

public class ShapeManager : IShapeService
{
    private readonly IShapeRepository _shapeRepository;
    private readonly ShapeBusinessRules _shapeBusinessRules;

    public ShapeManager(IShapeRepository shapeRepository, ShapeBusinessRules shapeBusinessRules)
    {
        _shapeRepository = shapeRepository;
        _shapeBusinessRules = shapeBusinessRules;
    }

    public async Task<Shape?> GetAsync(
        Expression<Func<Shape, bool>> predicate,
        Func<IQueryable<Shape>, IIncludableQueryable<Shape, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Shape? shape = await _shapeRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return shape;
    }

    public async Task<IPaginate<Shape>?> GetListAsync(
        Expression<Func<Shape, bool>>? predicate = null,
        Func<IQueryable<Shape>, IOrderedQueryable<Shape>>? orderBy = null,
        Func<IQueryable<Shape>, IIncludableQueryable<Shape, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Shape> shapeList = await _shapeRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return shapeList;
    }

    public async Task<Shape> AddAsync(Shape shape)
    {
        Shape addedShape = await _shapeRepository.AddAsync(shape);

        return addedShape;
    }

    public async Task<Shape> UpdateAsync(Shape shape)
    {
        Shape updatedShape = await _shapeRepository.UpdateAsync(shape);

        return updatedShape;
    }

    public async Task<Shape> DeleteAsync(Shape shape, bool permanent = false)
    {
        Shape deletedShape = await _shapeRepository.DeleteAsync(shape);

        return deletedShape;
    }
}
