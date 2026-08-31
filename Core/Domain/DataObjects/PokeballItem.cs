namespace Core.Domain.DataObjects
{
    public class PokeballItem : BaseItem
    {
        public int CatchPower { get; init; }
        public string? Description { get; init; }

        public PokeballItem(string id, string name, string category, int cost, int catchPower, string description)
            : base(id, name, category, cost)
        {
            CatchPower = catchPower;
            Description = description;
        }

        public PokeballItem() { } // Empty contstructor for Entity Framework
    }
}
