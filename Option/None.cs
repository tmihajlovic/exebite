namespace Option
{
    public sealed class None
    {
        public static None Value { get; } = new None();
        private None() { }
    }

    public class None<T> : Option<T>
    {
    }
}
