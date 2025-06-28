using Domain.Entities;
using Shared.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IShapeRepository : IAsyncRepository<Shape, Guid>, IRepository<Shape, Guid>
{
}