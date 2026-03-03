using Assets.Domain.Enums;

namespace Assets.Domain.Entities;


public sealed class SensorStatus
{
    public Guid AssetId { get; init; }
    public SensorStatusStatus Status { get; set; }
    public int HealthScore { get; set; }
    public DateTimeOffset LastTelemetryAt { get; set; }
}
