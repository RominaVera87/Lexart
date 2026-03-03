using Microsoft.EntityFrameworkCore;
using Assets.Domain.Entities;

namespace Assets.Infrastructure.Persistence;

public class AssetsDbContext : DbContext
{
    public AssetsDbContext(DbContextOptions<AssetsDbContext> options)
        : base(options)
    {
    }

    public DbSet<Asset> Assets => Set<Asset>();
    public DbSet<TelemetryLog> TelemetryLogs => Set<TelemetryLog>();
}