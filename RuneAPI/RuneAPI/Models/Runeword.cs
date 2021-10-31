using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RuneAPI.Models
{
    public class Runeword
    {
        public Runeword()
        {
            Modifiers = new HashSet<Modifier>();
            RunewordRunes = new HashSet<RunewordRune>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public long TargetTypes { get; set; }
        public long RequiredLevel { get; set; }

        public virtual ICollection<Modifier> Modifiers { get; set; }
        public virtual ICollection<RunewordRune> RunewordRunes { get; set; }
    }
}
