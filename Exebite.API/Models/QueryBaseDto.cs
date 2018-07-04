namespace Exebite.API.Models
{
    public abstract class QueryBaseDto
    {
        public int Size { get; set; }

        public int Page { get; set; }
    }
}
