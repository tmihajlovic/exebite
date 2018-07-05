using System.Collections.Generic;

namespace Exebite.DataAccess.Repositories
{
    public class PagingResult<T>
    {
        public PagingResult(IEnumerable<T> items, int total)
        {
            Total = total;
            Items = items;
        }

        public int Total { get; }

        public IEnumerable<T> Items { get; }

        public static PagingResult<T> Empty()
        {
            return new PagingResult<T>(new List<T>(), 0);
        }
    }
}
