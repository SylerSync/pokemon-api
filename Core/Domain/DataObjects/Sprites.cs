using Microsoft.SqlServer.Server;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.DataObjects
{
    public class Sprites
    {
        public string Front { get; set; }
        public string Back { get; set; }
        public string ShinyFront { get; set; }
        public string ShinyBack { get; set; }

        public Sprites(string front, string back, string shinyFront, string shinyBack) 
        {
            Front = front;
            Back = back;
            ShinyFront = shinyFront;
            ShinyBack = shinyBack;
        }

        public string Get(bool shiny, bool back) =>
        (shiny, back) switch
        {
            (false, false) => Front,
            (false, true) => Back,
            (true, false) => ShinyFront,
            (true, true) => ShinyBack
        };
    }
}
