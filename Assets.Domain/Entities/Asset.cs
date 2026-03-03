namespace Assets.Domain.Entities;

public class Asset
{
    public Guid Id { get; init; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public DateTimeOffset? CreatedAt { get; init; }
}
