using Core.Contracts.Pokemon;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Contracts.PokeBox
{
    public class PokeBoxRequest
    {
        public string UserID { get; set; }
        public PokemonFullInfoDto Pokemon { get; set; }
    }
}
