using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
