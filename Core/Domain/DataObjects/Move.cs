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
    public class Move
    {
        public string Name { get; set; }
        [BsonRepresentation(BsonType.String)]
        public List<TypeEnum> Types { get; set; }
        public int Power { get; set; }
        public int MaxPP { get; set; }
        public int CurrentPP { get; set; }
        public int Accuracy { get; set; }
        public int Priority { get; set; }
        [BsonRepresentation(BsonType.String)]
        public DamageClassEnum DamageClass { get; set; }
        public bool TargetSelf { get; set; }
        public int StatChance { get; set; } = 0;
        public string? Ailment { get; set; } = null;
        public int AilmentChance { get; set; } = 0;
        public int Drain { get; set; } = 0; // Negative is recoil
        public int Healing { get; set; } = 0; // % of Max HP healed
        public int FlinchChance { get; set; } = 0;
        public int CritRate { get; set; } = 0;
        public int MinTurns { get; set; } = 0;
        public int MaxTurns { get; set;} = 0;
        public int MinHits { get; set; } = 0;
        public int MaxHits { get; set; } = 0;
        [BsonRepresentation(BsonType.String)]
        public MoveCategoryEnum Catagory { get; set; }

        public Move(string name, List<TypeEnum> types, int power, int maxPP, int currentPP, int accuracy, int priority, DamageClassEnum damageClass, bool targetSelf, int statChance, string ailment, int ailmentChance, int drain, int healing, int flinchChance, int critRate, int minTurns, int maxTurns, int minHits, int maxHits, MoveCategoryEnum catagory)
        {
            Name = name;
            Types = types;
            Power = power;
            MaxPP = maxPP;
            CurrentPP = currentPP;
            Accuracy = accuracy;
            Priority = priority;
            DamageClass = damageClass;
            TargetSelf = targetSelf;
            StatChance = statChance;
            Ailment = ailment;
            AilmentChance = ailmentChance;
            Drain = drain;
            Healing = healing;
            FlinchChance = flinchChance;
            CritRate = critRate;
            MinTurns = minTurns;
            MaxTurns = maxTurns;
            MinHits = minHits;
            MaxHits = maxHits;
            Catagory = catagory;
        }

        public Move()
        {
        }
    }
}
