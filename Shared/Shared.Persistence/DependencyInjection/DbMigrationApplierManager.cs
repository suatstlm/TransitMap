using Microsoft.EntityFrameworkCore;

namespace Shared.Persistence.DependencyInjection;

public class DbMigrationApplierManager<TDbContext> : IDbMigrationApplierService<TDbContext>, IDbMigrationApplierService where TDbContext : DbContext
{
    private readonly TDbContext _context;

    public DbMigrationApplierManager(TDbContext context)
    {
        _context = context;
    }

    public void Initialize()
    {
        _context.Database.EnsureCreated();
    }
}


