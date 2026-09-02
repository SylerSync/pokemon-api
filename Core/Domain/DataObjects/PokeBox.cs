using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.DataObjects
{
    public class PokeBox
    {
        public PokeBox()
        {
        }

        public PokeBox(string userID, List<CaughtPokemon> pokemon)
        {
            UserID = userID;
            this.pokemon = pokemon;
        }

        public string UserID { get; set; }
        public List<CaughtPokemon> pokemon { get; set; }

    }
}
