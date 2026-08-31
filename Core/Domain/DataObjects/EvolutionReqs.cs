using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.DataObjects
{
    public class EvolutionReqs
    {
        public int? Level { get; set; }
        public string? Item { get; set; }
        public string? HeldItem { get; set; }
        public string? Trigger { get; set; }
        public string NextEvo { get; set; }

        public EvolutionReqs(int level, string item, string heldItem, string trigger, string nextEvo)
        {
            Level = level;
            Item = item;
            HeldItem = heldItem;
            Trigger = trigger;
            NextEvo = nextEvo;
        }

        public EvolutionReqs()
        {
        }
    }
}
