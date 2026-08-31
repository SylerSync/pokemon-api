namespace Core.Contracts.Item
{
    public class ItemDto
    {
        // Common base properties of itmes
        public string Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int Cost { get; set; }

        //Specialized Nullable Properties for different item types
        public string EffectType { get; set; }
        public int? Amount { get; set; }
        public string Status { get; set; }
        public string Scope { get; set; }
        public int? Stages { get; set; }
        public int? CatchPower { get; set; }
        public float? Percent {  get; set; }
        public string Move { get; set; }
        public string MoveName { get; set; }
        public string PokemonName { get; set; } 
        public string MegaFormName { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }

    }
}
