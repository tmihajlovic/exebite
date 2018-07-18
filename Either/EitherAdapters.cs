using System;

namespace Either
{
    public static class EitherAdapters
    {
        public static Either<TLeft, TNewRight> Map<TLeft, TRight, TNewRight>(
            this Either<TLeft, TRight> either, Func<TRight, TNewRight> map) =>
            either is Right<TLeft, TRight> right
                ? (Either<TLeft, TNewRight>)map(right)
                : (TLeft)(Left<TLeft, TRight>)either;

        public static Either<TLeft, TNewRight> Map<TLeft, TRight, TNewRight>(
            this Either<TLeft, TRight> either, Func<TRight, Either<TLeft, TNewRight>> map) =>
            either is Right<TLeft, TRight> right
                ? map(right)
                : (TLeft)(Left<TLeft, TRight>)either;

        public static Either<TLeft, TNewRight> Map<TLeft, TRight, TNewRight>(
            this Either<TLeft, TRight> either, Func<TRight, TNewRight> map, Action<TRight> logAction)
        {
            switch (either)
            {
                case Right<TLeft, TRight> right:
                    logAction(right);
                    return map(right);
                default:
                    return (TLeft)(Left<TLeft, TRight>)either;
            }
        }


        public static Either<TLeft, TNewRight> Map<TLeft, TRight, TNewRight>(
            this Either<TLeft, TRight> either, Func<TRight, Either<TLeft, TNewRight>> map, Action<TRight> logAction)
        {
            switch (either)
            {
                case Right<TLeft, TRight> right:
                    logAction(right);
                    return map(right);
                default:
                    return (TLeft)(Left<TLeft, TRight>)either;
            }
        }

        public static TRight Reduce<TLeft, TRight>(
            this Either<TLeft, TRight> either, Func<TLeft, TRight> map) =>
            either is Left<TLeft, TRight> left
                ? map(left)
                : (Right<TLeft, TRight>)either;

        public static Either<TLeft, TRight> Reduce<TLeft, TRight>(
            this Either<TLeft, TRight> either, Func<TLeft, TRight> map,
            Func<TLeft, bool> when) =>
            either is Left<TLeft, TRight> bound && when(bound)
                ? (Either<TLeft, TRight>)map(bound)
                : either;

        public static TRight Reduce<TLeft, TRight>(
            this Either<TLeft, TRight> either, Func<TLeft, TRight> map, Action<TLeft> logAction)
        {
            switch (either)
            {
                case Left<TLeft, TRight> left:
                    logAction(left);
                    return map(left);
                default:
                    return (Right<TLeft, TRight>)either;
            }
        }

        public static Either<TLeft, TRight> Reduce<TLeft, TRight>(
            this Either<TLeft, TRight> either, Func<TLeft, TRight> map,
            Func<TLeft, bool> when, Action<TLeft> action)
        {
            switch (either)
            {
                case Left<TLeft, TRight> bound when when(bound):
                    action(bound);
                    return (Either<TLeft, TRight>)map(bound);
                default:
                    return either;
            }
        }
    }
}
