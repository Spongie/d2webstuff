namespace RuneAPI.Models
{
    public class Modifier
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public static implicit operator Modifier(string stringValue)
        {
            return new Modifier
            {
                Text = stringValue
            };
        }
    }
}
