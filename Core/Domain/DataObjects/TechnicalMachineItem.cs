namespace Core.Domain.DataObjects
{
    public class TechnicalMachineItem : Item
    {
        public string Move {  get; private set; }
        public string MoveName { get; private set; }
        public string Type { get; private set; }

        public TechnicalMachineItem(string id, string name, string category, int cost, string move, string moveName, string type): base(id, name, category, cost)
        {
            Move = move;
            MoveName = moveName;
            Type = type;
        }
    }
}
