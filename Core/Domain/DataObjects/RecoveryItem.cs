namespace Core.Domain.DataObjects
{
    public class RecoveryItem : Item
    {
        public ItemEffect Effect { get; private set; }

        public RecoveryItem(string id, string name, string category, int cost, ItemEffect effect)
            :base(id,name,category,cost)
        {
            Effect = effect;
        }
    }
}
