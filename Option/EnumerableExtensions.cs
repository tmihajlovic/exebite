using System;
using System.Collections.Generic;
using System.Linq;

namespace Option
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<TResult> Flatten<T, TResult>(
            this IEnumerable<T> sequence, Func<T, Option<TResult>> map) =>
            sequence.Select(map)
                .OfType<Some<TResult>>()
                .Select(x => (TResult)x);

        public static Option<T> FirstOrNone<T>(
            this IEnumerable<T> sequence, Func<T, bool> predicate) =>
            sequence.Where(predicate)
                .Select<T, Option<T>>(x => x)
                .DefaultIfEmpty(None.Value)
                .First();

        public static Option<T> FirstOrNone<T>(
            this IEnumerable<T> sequence) =>
            sequence.Select<T, Option<T>>(x => x)
                .DefaultIfEmpty(None.Value)
                .First();
    }
}
