using System.Collections.Generic;
using System.Text.Json.Serialization;

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
        [JsonIgnore]
        public virtual ICollection<RunewordRune> RunewordRunes { get; set; }
    }
}
