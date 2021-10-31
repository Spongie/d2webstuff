using System.Collections.Generic;

namespace RuneAPI.Models
{
    public class Rune
    {
        public Rune()
        {
            RunewordRunes = new HashSet<RunewordRune>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public long Number { get; set; }
        public string ImagePath { get; set; }

        public virtual ICollection<RunewordRune> RunewordRunes { get; set; }
    }
}
