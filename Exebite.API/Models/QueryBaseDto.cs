namespace Exebite.API.Models
{
    public abstract class QueryBaseDto
    {
        protected QueryBaseDto(int page, int size)
        {
            Size = size;
            Page = page;
        }

        public int Size { get; }

        public int Page { get; }
    }
}
