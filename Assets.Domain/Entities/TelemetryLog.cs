namespace Assets.Domain.Entities;

public sealed class TelemetryLog
{
    public Guid Id { get; init; }
    public Guid AssetId { get; init; }
    public DateTimeOffset Timestamp { get; set; }
    public int HealthScore { get; set; }
    public Asset Asset { get; set; } = null!;
}
