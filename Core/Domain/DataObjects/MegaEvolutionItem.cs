namespace Core.Domain.DataObjects
{
    public class MegaEvolutionItem : BaseItem
    {

        public string PokemonName { get; init; }
        public string MegaFormName { get; init; }
        public string Description { get; init; }

        public MegaEvolutionItem(string id, string name, string category, int cost, string pokemonName, string megaName, string description) : base(id, name, category, cost)
        {
            PokemonName = pokemonName;
            MegaFormName = megaName;
            Description = description;
        }
    }
}
