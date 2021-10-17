using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RuneAPI.Models
{
    public class Runeword
    {
        public string Name { get; set; }
        public Modifier[] Modifiers { get; set; }
        public Rune[] Runes { get; set; }
    }
}
