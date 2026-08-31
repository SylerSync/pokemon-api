namespace Core.Domain.DataObjects;

public class RecoveryItem : BaseItem
{
    public ItemEffect Effect { get; set; } = null!;

    // Parameterless constructor for EF Core
    public RecoveryItem() { }

    // Domain constructor
    public RecoveryItem(string id, string name, string category, int cost, ItemEffect effect)
        : base(id, name, category, cost)
    {
        Effect = effect;
    }
}