namespace Core.Domain.DataObjects
{
    public abstract class BaseItem
    {
        public string Id { get; init; } = string.Empty;
        public string Name { get; init; } = string.Empty;
        public string Category { get; init; } = string.Empty;
        public int Cost { get; protected set; } = 0;

        protected BaseItem(string id, string name, string category, int cost)
        {
            Id = id;
            Name = name;
            Category = category;
            Cost = cost;
        }

        public BaseItem() { } // Empty contstructor for Entity Framework

    }


}
