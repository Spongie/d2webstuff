namespace RuneAPI.Models
{
    public class RunewordRune
    {
        public long Id { get; set; }
        public long? RunewordId { get; set; }
        public long? RuneId { get; set; }

        public virtual Rune Rune { get; set; }
        public virtual Runeword Runeword { get; set; }
    }
}
