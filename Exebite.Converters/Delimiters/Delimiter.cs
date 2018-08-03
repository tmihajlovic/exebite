namespace Exebite.Converters.Delimiters
{
    public abstract class Delimiter
    {
        public string Value { get; }

        protected Delimiter(string delimiter)
        {
            Value = delimiter;
        }
    }
}
