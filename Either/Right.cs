namespace Either
{
    public class Right<TLeft, TRight> : Either<TLeft, TRight>
    {
        private TRight Content { get; }

        public Right(TRight content)
        {
            this.Content = content;
        }

        public static implicit operator TRight(Right<TLeft, TRight> obj) =>
            obj.Content;
    }
}
