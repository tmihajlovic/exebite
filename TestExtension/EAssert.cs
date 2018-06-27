using Either;
using Option;
using Xunit;

namespace Optional.Xunit
{
    public static class EAssert
    {
        public static void IsRight<TLeft, TRight>(Either<TLeft, TRight> either)
        {
            Assert.IsType<Right<TLeft, TRight>>(either);
        }

        public static void IsLeft<TLeft, TRight>(Either<TLeft, TRight> either)
        {
            Assert.IsType<Left<TLeft, TRight>>(either);
        }

        public static void IsNone<T>(Option<T> option)
        {
            Assert.IsType<None<T>>(option);
        }

        public static void IsSome<T>(Option<T> option)
        {
            Assert.IsType<Some<T>>(option);
        }

        /// <summary>
        /// Only use in test!
        /// </summary>
        /// <typeparam name="E"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public static T RightContent<E, T>(this Either<E, T> item)
        {
            return item as Right<E, T>;
        }


    }
}

