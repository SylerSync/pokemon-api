using Core.Contracts.Pokemon;
using Core.Domain.DataObjects;
using Core.Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Contracts.PokeBox
{
    public class PokeBoxDto
    {
        public List<PokemonInBoxDto> pokemon { get; set; }
    }

    public class PokemonInBoxDto() 
    {
        public string _id { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public bool Shiny { get; set; }
        public Sprites Sprites { get; set; }
        public List<string> Types { get; set; } = new List<string>();
        public int Height { get; set; }
        public int Weight { get; set; }
        public string Cry { get; set; }
        public int CaptureRate { get; set; }
        public int TotalHP { get; set; }
        public int CurrentHP { get; set; }
        public List<StatDto> Stats { get; set; }
        public List<Move> Moves { get; set; } = new List<Move>();
        public List<string> LearnableMoves { get; set; } = new List<string>();
        public int TotalKOs { get; set; }
        public int TotalFaints { get; set; }
        public int Level { get; set; }
        public List<EvolutionReqs> EvolutionReqs { get; set; }
        public int BaseExp { get; set; }
        public int CurrentExp { get; set; }
        public string? Status { get; set; }
    }
}
