using System;

namespace Exebite.DataAccess.Repositories
{



    public abstract class QueryBase
    {
        public QueryBase()
        {
            Page = 0;
            Size = QueryConstants.MaxElements;
        }

        public QueryBase(int page, int size)
        {
            if (size > QueryConstants.MaxElements)
            {
                throw new ArgumentOutOfRangeException("Size is too big");
            }

            Page = page;
            Size = QueryConstants.MaxElements;
        }

        public int Size { get; }

        public int Page { get; }
    }
}