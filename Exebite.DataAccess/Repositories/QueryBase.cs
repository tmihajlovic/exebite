using System;

namespace Exebite.DataAccess.Repositories
{



    public abstract class QueryBase
    {
        public QueryBase()
        {
            Page = 0;
            Size = int.MaxValue;
        }

        public QueryBase(int page, int size)
        {
            Size = size;
            Page = page;
        }

        public int Size { get; }

        public int Page { get; }
    }
}