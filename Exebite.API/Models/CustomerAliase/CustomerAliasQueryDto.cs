namespace Exebite.API.Models
{
    public class CustomerAliasQueryDto : QueryBaseDto
    {
        public int? Id { get; set; }

        public string GoogleUserId { get; set; }
    }
}
