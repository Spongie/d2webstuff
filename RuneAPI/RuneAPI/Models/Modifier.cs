namespace RuneAPI.Models
{
    public class Modifier
    {
        public string FormatString { get; set; }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
        
        public string toFormattedString()
        {
            return string.Format(FormatString, MinValue, MaxValue);
        }
    }
}
