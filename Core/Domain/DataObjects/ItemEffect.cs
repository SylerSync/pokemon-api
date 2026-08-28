namespace Core.Domain.DataObjects;

// Abstract Base Effect
public abstract class ItemEffect
{
    public string EffectType { get; init; } = string.Empty;
}

// Derived Concrete Effects
public class HealEffect : ItemEffect
{
    public int Amount { get; init; }

    public HealEffect()
    {
        EffectType = "heal";
    }
}

public class ReviveEffect : ItemEffect
{
    public float Percent { get; init; }
    public ReviveEffect()
    {
        EffectType = "revive";
    }
}

public class StatusHealEffect : ItemEffect
{
    public string Status { get; init; } = string.Empty;

    public StatusHealEffect()
    {
        EffectType = "status-heal";
    }
}

public class PpHealEffect : ItemEffect
{
    public string Scope { get; init; } = string.Empty;
    public int Amount { get; init; }

    public PpHealEffect()
    {
        EffectType = "pp-heal";
    }
}

public class PpMaxRaise : ItemEffect
{
    public string Scope { get; init; } = string.Empty;
    public int Stages { get; init; }

    public PpMaxRaise()
    {
        EffectType = "pp-max-raise";
    }
}