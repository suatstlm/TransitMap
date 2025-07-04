using Application.Services.Repositories;
using Domain.Entities;
using Shared.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ShapeRepository : EfRepositoryBase<Shape, Guid, BaseDbContext>, IShapeRepository
{
    public ShapeRepository(BaseDbContext context) : base(context)
    {
    }
}