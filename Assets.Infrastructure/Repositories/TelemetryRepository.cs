using Assets.Application.Interfaces;
using Assets.Domain.Entities;
using Assets.Domain.Enums;
using Assets.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Assets.Infrastructure.Repositories;

public class TelemetryRepository : ITelemetryRepository
{
    private readonly AssetsDbContext _context;

    public TelemetryRepository(AssetsDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<TelemetryLog>> ListAsync(CancellationToken cancellationToken)
    {
        return await _context.TelemetryLogs
            .OrderByDescending(t => t.Timestamp)
            .ToListAsync(cancellationToken);
    }

    public async Task<TelemetryLog?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.TelemetryLogs
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public async Task<TelemetryLog> CreateAsync(TelemetryLog telemetryLog, CancellationToken cancellationToken)
    {
        _context.TelemetryLogs.Add(telemetryLog);
        await _context.SaveChangesAsync(cancellationToken); 
        return telemetryLog;
    }

    public async Task<TelemetryLog> UpdateAsync(TelemetryLog telemetryLog, CancellationToken cancellationToken)
    {
        var existing = _context.TelemetryLogs
            .FirstOrDefaultAsync(r => telemetryLog.Id == telemetryLog.Id, cancellationToken);

        if(existing == null)
        {
            return null;
        }

        await _context.SaveChangesAsync(cancellationToken);
        return telemetryLog;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var exisiting = await _context.TelemetryLogs
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

        if(exisiting == null)
        {
            return false;
        }

        _context.TelemetryLogs.Remove(exisiting);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<SensorStatus?> GetSensorStatusAsync(Guid assetId, CancellationToken cancellationToken)
    {
        var status = await _context.TelemetryLogs
            .Where(t => t.AssetId == assetId)
            .OrderByDescending(t =>t.Timestamp)
            .FirstOrDefaultAsync(cancellationToken);

        if(status is null)
        {
            return null;
        }

        return new SensorStatus
        {
            AssetId = assetId,
            HealthScore = status.HealthScore,
            Status = status.HealthScore >= 80 ? AlertLevel.Ok
            : status.HealthScore >= 50 ? AlertLevel.Warning
                : AlertLevel.Critical,
            LastTelemetryAt = status.Timestamp
        };
    }
}
