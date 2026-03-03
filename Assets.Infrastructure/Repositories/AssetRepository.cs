using Assets.Application.Interfaces;
using Assets.Domain.Entities;
using Assets.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Assets.Infrastructure.Repositories;

public class AssetRepository : IAssetRepository
{
    private readonly AssetsDbContext _context;

    public AssetRepository(AssetsDbContext context)
    {
        _context = context;
    }

    public async Task<Asset?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Assets
            .Include(a => a.TelemetryLogs)
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Asset>> ListAsync(CancellationToken cancellationToken)
    {
        return await _context.Assets.ToListAsync(cancellationToken);
    }

    public async Task<Asset> CreateAsync(Asset asset, CancellationToken cancellationToken)
    {
        _context.Assets.Add(asset);
        await _context.SaveChangesAsync(cancellationToken);
        return asset;
    }

    public async Task<Asset> UpdateAsync(Asset asset, CancellationToken cancellationToken)
    {
        var existing = await _context.Assets.FindAsync(new object[] { asset.Id }, cancellationToken);
        if (existing is null)
        {
            return null;
        }

        existing.Name = asset.Name;
        existing.Type = asset.Type;
        return existing;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var asset = await _context.Assets.FindAsync(new object[] { id }, cancellationToken);
        if (asset is null)
        {
            return false;
        }
        _context.Assets.Remove(asset);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
