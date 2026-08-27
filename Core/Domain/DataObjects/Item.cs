namespace Core.Domain.DataObjects
{
    public abstract class Item
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Category { get; private set; }
        public int Cost { get; private set; }

        protected Item(string id, string name, string category, int cost)
        {
            Id = id;
            Name = name;
            Category = category;
            Cost = cost;
        }

    }


}
