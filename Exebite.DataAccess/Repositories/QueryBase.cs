namespace Exebite.DataAccess.Repositories
{
    public abstract class QueryBase
    {
        protected QueryBase()
        {
            Page = 0;
            Size = int.MaxValue;
        }

        protected QueryBase(int page, int size)
        {
            Size = size;
            Page = page;
        }

        public int Size { get; }

        public int Page { get; }
    }
}