namespace Exebite.API.Models
{
    public class LocationQueryDto : QueryBaseDto
    {
        public LocationQueryDto(int page, int size)
            : base(page, size)
        {
        }

        public int? Id { get; set; }
    }
}
