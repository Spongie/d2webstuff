namespace RuneAPI.Models
{
    public class Modifier
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public long? RunewordId { get; set; }

        public virtual Runeword Runeword { get; set; }

        public static implicit operator Modifier(string stringValue)
        {
            return new Modifier
            {
                Text = stringValue
            };
        }
    }
}
