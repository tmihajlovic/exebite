namespace Exebite.DtoModels
{
    public class CustomerQueryDto : QueryBaseDto
    {
        public long? Id { get; set; }

        public string GoogleUserId { get; set; }
    }
}
