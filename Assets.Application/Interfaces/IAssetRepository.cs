using Assets.Domain.Entities;

namespace Assets.Application.Interfaces;

public interface IAssetRepository
{
    Task<IReadOnlyList<Asset>> ListAsync(CancellationToken cancellationToken);
    Task<Asset?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Asset> CreateAsync(Asset asset, CancellationToken cancellationToken);
    Task<Asset> UpdateAsync(Asset asset, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}
