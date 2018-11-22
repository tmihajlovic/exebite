namespace Exebite.DtoModels
{
    public class CustomerQueryDto : QueryBaseDto
    {
        public int? Id { get; set; }

        public string GoogleUserId { get; set; }
    }
}
