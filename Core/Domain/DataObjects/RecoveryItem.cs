namespace Core.Domain.DataObjects
{
    public class RecoveryItem : BaseItem
    {
        public ItemEffect Effect { get; init; }

        public RecoveryItem(string id, string name, string category, int cost, ItemEffect effect)
            :base(id,name,category,cost)
        {
            Effect = effect;
        }
    }
}
