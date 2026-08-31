namespace Core.Domain.DataObjects;

public class ItemEffect
{
    public string EffectType { get; set; } = string.Empty;
    public int? Amount { get; set; }
    public float? Percent { get; set; }
    public string? Status { get; set; }
    public string? Scope { get; set; }
    public int? Stages { get; set; }

    public ItemEffect() { }
}