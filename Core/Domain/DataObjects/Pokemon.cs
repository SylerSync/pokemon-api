using Core.Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.DataObjects
{
    public class Pokemon
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public bool Shiny { get; set; }
        public Sprites Sprites { get; set; }
        public List<TypeEnum> Types { get; set; } = new List<TypeEnum>();
        public int Height { get; set; }
        public int Weight { get; set; }
        public string Cry { get; set; }
        public int CaptureRate { get; set; }
        public int TotalHP { get; set; }
        public int CurrentHP { get; set; }
        public List<Stat> Stats { get; set; }
        public List<Move> Moves { get; set; } = new List<Move>();
        public int TotalKOs { get; set; }
        public int TotalFaints { get; set; }
        public int Level { get; set; }
        public EvolutionReqs[] EvolutionReqs { get; set; }
        public int BaseExp { get; set; }
        public int CurrentExp { get; set; }
        public string[] MinorStatus { get; set; } = Array.Empty<string>();

        public Pokemon(string _idvalue, int id, string name, bool shiny, Sprites sprites, List<TypeEnum> types, int height, int weight, string cry, int captureRate, int totalHP, int currentHP, List<Stat> stats, List<Move> moves, int totalKOs, int totalFaints, int level, EvolutionReqs[] evolutionReqs, int baseExp, int currentExp, string[] minorStatus)
        {
            _id = _idvalue;
            ID = id;
            Name = name;
            Shiny = shiny;
            Sprites = sprites;
            Types = types;
            Height = height;
            Weight = weight;
            Cry = cry;
            CaptureRate = captureRate;
            TotalHP = totalHP;
            CurrentHP = currentHP;
            Stats = stats;
            Moves = moves;
            TotalKOs = totalKOs;
            TotalFaints = totalFaints;
            Level = level;
            EvolutionReqs = evolutionReqs;
            BaseExp = baseExp;
            CurrentExp = currentExp;
            MinorStatus = minorStatus;
        }
    }
}
