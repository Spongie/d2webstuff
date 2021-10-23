using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RuneAPI.Models
{
    public class Runeword
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Modifier> Modifiers { get; set; }
        public virtual ICollection<Rune> Runes { get; set; }
        public ItemType TargetTypes { get; set; }
    }
}
