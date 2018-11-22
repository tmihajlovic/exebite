namespace Either
{
    public class Left<TLeft, TRight> : Either<TLeft, TRight>
    {
        private TLeft Content { get; }

        public Left(TLeft content)
        {
            this.Content = content;
        }

        public static implicit operator TLeft(Left<TLeft, TRight> obj) =>
            obj.Content;
    }
}
