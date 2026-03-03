using Assets.Domain.Entities;

namespace Assets.Application.Interfaces;

public interface ITelemetryRepository
{
    Task<IReadOnlyList<TelemetryLog>> ListAsync(CancellationToken cancellationToken);
    Task<TelemetryLog?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<TelemetryLog> CreateAsync(TelemetryLog telemetryLog, CancellationToken cancellationToken);
    Task<TelemetryLog> UpdateAsync(TelemetryLog telemetryLog, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task<SensorStatus?> GetSensorStatusAsync(Guid assetId, CancellationToken cancellationToken);
}
