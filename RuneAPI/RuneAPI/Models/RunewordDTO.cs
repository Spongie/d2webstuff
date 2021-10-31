using System.Collections.Generic;
using System.Linq;

namespace RuneAPI.Models
{
    public class RunewordDTO
    {
        public RunewordDTO()
        {

        }

        public RunewordDTO(Runeword runeword)
        {
            Id = runeword.Id;
            Name = runeword.Name;
            TargetTypes = runeword.TargetTypes;
            RequiredLevel = runeword.RequiredLevel;
            Runes = runeword.RunewordRunes.Select(r => r.Rune).ToList();
            Modifiers = runeword.Modifiers.ToList();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public long TargetTypes { get; set; }
        public long RequiredLevel { get; set; }
        public List<Modifier> Modifiers { get; set; }
        public List<Rune> Runes { get; set; }
    }
}
