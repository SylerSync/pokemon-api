namespace Core.Domain.DataObjects
{
    public class TechnicalMachineItem : BaseItem
    {
        public string Move {  get; init; }
        public string MoveName { get; init; }
        public string Type { get; init; }

        public TechnicalMachineItem(string id, string name, string category, int cost, string move, string moveName, string type): base(id, name, category, cost)
        {
            Move = move;
            MoveName = moveName;
            Type = type;
        }
    }
}
