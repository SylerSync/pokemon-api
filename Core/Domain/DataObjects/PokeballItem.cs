namespace Core.Domain.DataObjects
{
    public class PokeballItem : BaseItem
    {
        public int CatchPower { get; private set; }
        public string Description { get; private set; }

        public PokeballItem(string id, string name, string category, int cost, int catchPower, string description)
            : base(id, name, category, cost)
        {
            CatchPower = catchPower;
            Description = description;
        }
    }
}
