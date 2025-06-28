using Shared.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Shapes;

public interface IShapeService
{
    Task<Shape?> GetAsync(
        Expression<Func<Shape, bool>> predicate,
        Func<IQueryable<Shape>, IIncludableQueryable<Shape, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Shape>?> GetListAsync(
        Expression<Func<Shape, bool>>? predicate = null,
        Func<IQueryable<Shape>, IOrderedQueryable<Shape>>? orderBy = null,
        Func<IQueryable<Shape>, IIncludableQueryable<Shape, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Shape> AddAsync(Shape shape);
    Task<Shape> UpdateAsync(Shape shape);
    Task<Shape> DeleteAsync(Shape shape, bool permanent = false);
}
