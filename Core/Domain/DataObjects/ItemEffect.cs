namespace Core.Domain.DataObjects
{
    // Abstract class for the different types of Healing item effects
    public abstract class ItemEffect
    {
        public string EffectType { get; protected set; }
        protected ItemEffect(string effectType)
        {
            EffectType = effectType;
        }
    }

    public class HealEffect : ItemEffect
    {
        public int Amount { get; private set; }
        public HealEffect(int amount) : base("heal")
        {
            Amount = amount;
        }
    }

    public class StatusHealEffect : ItemEffect
    {
        public string Status { get; private set; }

        public StatusHealEffect(string status) : base("status-heal")
        {
            Status = status;
        }
    }

    public class PpHealEffect : ItemEffect
    {
        public string Scope { get; private set; }
        public int Amount { get; private set; }

        public PpHealEffect(string scope, int amount): base("pp-heal")
        {
            Scope = scope;
            Amount = amount;
        }
    }

    public class PpMaxRaise : ItemEffect
    {
        public string Scope { get; private set; }
        public int Stages { get; private set; }
        public PpMaxRaise(string scope, int stages): base("pp-max-raise")
        {
            Scope = scope;
            Stages = stages;
        }
    }
}
