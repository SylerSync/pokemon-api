namespace Core.Domain.DataObjects
{
    public class EvolutionItem : BaseItem
    {
        public string Description { get; private set; }

        public EvolutionItem(string id, string name, string category, int cost, string description):base(id, name, category, cost)
        {
            Description = description;
        }
    }
}
