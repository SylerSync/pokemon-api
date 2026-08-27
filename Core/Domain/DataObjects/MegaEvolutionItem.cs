namespace Core.Domain.DataObjects
{
    public class MegaEvolutionItem : BaseItem
    {

        public string PokemonName { get; private set; }
        public string MegaFormName { get; private set; }
        public string Description { get; private set; }

        public MegaEvolutionItem(string id, string name, string category, int cost, string pokemonName, string megaName, string description) : base(id, name, category, cost)
        {
            PokemonName = pokemonName;
            MegaFormName = megaName;
            Description = description;
        }
    }
}
