using Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.DataObjects
{
    public class Stat
    {
        public StatEnum Name { get; set; }
        public int BaseStat { get; set; }
        public int StatTotal { get; set; }

        public Stat(StatEnum name, int baseStat, int statTotal)
        {
            Name = name;
            BaseStat = baseStat;
            StatTotal = statTotal;
        }
    }
}
