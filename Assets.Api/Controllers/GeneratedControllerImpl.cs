using Assets.Api.Controllers.Generated;
using Assets.Application.Interfaces;
using Assets.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Assets.Api.Controllers;

public class GeneratedControllerImpl : IController
{
    private readonly IAssetRepository _assets;
    private readonly ITelemetryRepository _telemetry;

    public GeneratedControllerImpl(IAssetRepository assets, ITelemetryRepository telemetry)
    {
        _assets = assets;
        _telemetry = telemetry;
    }

    public async Task<ActionResult<Asset>> CreateAssetAsync(AssetCreateRequest body)
    {
        var domain = new Assets.Domain.Entities.Asset
        {
            Id = Guid.NewGuid(),
            Name = body.Name,
            Type = body.Type,
            TelemetryLogs = new List<Assets.Domain.Entities.TelemetryLog>()
        };

        var created = await _assets.CreateAsync(domain, CancellationToken.None);

        var result = new Asset
        {
            Id = created.Id,
            Name = created.Name,
            Type = created.Type
        };

        return new CreatedResult($"/api/assets/{result.Id}", result);
    }

    public async Task<ActionResult<TelemetryLog>> CreateTelemetryLogAsync(TelemetryLogCreateRequest body, Guid assetId, DateTimeOffset? from, DateTimeOffset? to)
    {
        var asset = await _assets.GetByIdAsync(assetId, CancellationToken.None);
        if (asset is null)
            return new NotFoundResult();

        var domain = new Assets.Domain.Entities.TelemetryLog
        {
            Id = Guid.NewGuid(),
            AssetId = assetId,
            Timestamp = body.Timestamp,
            HealthScore = body.HealthScore
        };

        var created = await _telemetry.CreateAsync(domain, CancellationToken.None);

        var result = new TelemetryLog
        {
            Id = created.Id,
            AssetId = created.AssetId,
            Timestamp = created.Timestamp,
            HealthScore = created.HealthScore
        };

        return new CreatedResult($"/api/assets/{assetId}/telemetry/{result.Id}", result);
    }

    public async Task<IActionResult> DeleteAssetAsync(Guid assetId)
    {
        var deleted = await _assets.DeleteAsync(assetId, CancellationToken.None);
        if (!deleted)
            return new NotFoundResult();

        return new OkResult();
    }

    public async Task<IActionResult> DeleteTelemetryLogAsync(Guid assetId, Guid logId)
    {
        var log = await _telemetry.GetByIdAsync(logId, CancellationToken.None);
        if (log is null || log.AssetId != assetId)
            return new NotFoundResult();

        var deleted = await _telemetry.DeleteAsync(logId, CancellationToken.None);
        if (!deleted)
            return new NotFoundResult();

        return new OkResult();
    }

    public async Task<ActionResult<Asset>> GetAssetByIdAsync(Guid assetId)
    {
        var asset = await _assets.GetByIdAsync(assetId, CancellationToken.None);
        if (asset is null)
            return new NotFoundResult();

        var result = new Asset
        {
            Id = asset.Id,
            Name = asset.Name,
            Type = asset.Type
        };

        return new OkObjectResult(result);
    }

    public async Task<ActionResult<SensorStatus>> GetSensorStatusAsync(Guid assetId)
    {
        var asset = await _assets.GetByIdAsync(assetId, CancellationToken.None);
        if (asset is null)
            return new NotFoundResult();

        var status = await _telemetry.GetSensorStatusAsync(assetId, CancellationToken.None);
        if (status is null)
            return new NotFoundResult();

        var result = new SensorStatus
        {
            AssetId = status.AssetId,
            HealthScore = status.HealthScore,
            Status = status.HealthScore >= 80 ? SensorStatusStatus.Ok
                : status.HealthScore >= 50 ? SensorStatusStatus.Warning
                : SensorStatusStatus.Critical,
            LastTelemetryAt = status.LastTelemetryAt
        };

        return new OkObjectResult(result);
    }

    public async Task<ActionResult<TelemetryLog>> GetTelemetryLogByIdAsync(Guid assetId, Guid logId)
    {
        var log = await _telemetry.GetByIdAsync(logId, CancellationToken.None);
        if (log is null || log.AssetId != assetId)
            return new NotFoundResult();

        var result = new TelemetryLog
        {
            Id = log.Id,
            AssetId = log.AssetId,
            Timestamp = log.Timestamp,
            HealthScore = log.HealthScore
        };

        return new OkObjectResult(result);
    }

    public async Task<ActionResult<ICollection<Asset>>> ListAssetsAsync()
    {
        var list = await _assets.ListAsync(CancellationToken.None);

        var result = list.Select(a => new Asset
        {
            Id = a.Id,
            Name = a.Name,
            Type = a.Type
        }).ToList();

        return new OkObjectResult(result);
    }

    public async Task<ActionResult<ICollection<TelemetryLog>>> ListTelemetryLogsAsync(Guid assetId, DateTimeOffset? from, DateTimeOffset? to)
    {
        var asset = await _assets.GetByIdAsync(assetId, CancellationToken.None);
        if (asset is null)
            return new NotFoundResult();

        var logs = await _telemetry.ListAsync(CancellationToken.None);

        var result = logs
            .Where(t => t.AssetId == assetId)
            .Where(t => from is null || t.Timestamp >= from)
            .Where(t => to is null || t.Timestamp <= to)
            .Select(t => new TelemetryLog
            {
                Id = t.Id,
                AssetId = t.AssetId,
                Timestamp = t.Timestamp,
                HealthScore = t.HealthScore
            })
            .ToList();

        return new OkObjectResult(result);
    }

    public async Task<ActionResult<Asset>> UpdateAssetAsync(AssetUpdateRequest body, Guid assetId)
    {
        var existing = await _assets.GetByIdAsync(assetId, CancellationToken.None);
        if (existing is null)
            return new NotFoundResult();

        existing.Name = body.Name;
        existing.Type = body.Type;

        var updated = await _assets.UpdateAsync(existing, CancellationToken.None);
        if (updated is null)
        {
            return new NotFoundResult();
        }

        var result = new Asset
        {
            Id = updated.Id,
            Name = updated.Name,
            Type = updated.Type
        };

        return new OkObjectResult(result);
    }

    public async Task<ActionResult<TelemetryLog>> UpdateTelemetryLogAsync(TelemetryLogUpdateRequest body, Guid assetId, Guid logId)
    {
        var log = await _telemetry.GetByIdAsync(logId, CancellationToken.None);
        if (log is null || log.AssetId != assetId)
            return new NotFoundResult();

        log.Timestamp = body.Timestamp;
        log.HealthScore = body.HealthScore;

        var updated = await _telemetry.UpdateAsync(log, CancellationToken.None);

        if (updated is null)
        {
            return new NotFoundResult();
        }

        var result = new TelemetryLog
        {
            Id = updated.Id,
            AssetId = updated.AssetId,
            Timestamp = updated.Timestamp,
            HealthScore = updated.HealthScore
        };

        return new OkObjectResult(result);
    }
}
