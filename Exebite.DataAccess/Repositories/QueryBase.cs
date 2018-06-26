using System;

namespace Exebite.DataAccess.Repositories
{
    public abstract class QueryBase
    {

        public QueryBase()
        {
            Page = 0;
            Size = 100;
        }

        public QueryBase(int page, int size)
        {
            if (size > 10000)
            {
                throw new ArgumentOutOfRangeException("Size is too big");
            }

            Page = page;
            Size = size;
        }

        public int Size { get; }

        public int Page { get; }
    }
}