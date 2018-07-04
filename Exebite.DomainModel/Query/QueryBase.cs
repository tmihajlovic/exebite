namespace Exebite.DomainModel
{
    public abstract class QueryBase
    {
        protected QueryBase()
        {
            Page = 1;
            Size = QueryConstants.MaxElements;
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