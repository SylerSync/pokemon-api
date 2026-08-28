using Core.Domain.DataObjects;
using Core.Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contracts.Pokemon
{
    public class PokemonDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Sprites Sprites { get; set; }
        public List<TypeEnum> Types { get; set; } = new List<TypeEnum>();
        public int Height { get; set; }
        public int Weight { get; set; }
        public string Cry { get; set; }
        public List<Stat> Stats { get; set; }
        public List<EvolutionReqs> EvolutionReqs { get; set; } = new List<EvolutionReqs>();
    }
}
