using Microsoft.EntityFrameworkCore;

namespace Persistence.DependencyInjection;

public interface IDbMigrationApplierService
{
    void Initialize();
}

public interface IDbMigrationApplierService<TDbContext> : IDbMigrationApplierService where TDbContext : DbContext
{
}


