namespace Exebite.DtoModels
{
    public class CustomerAliasQueryDto : QueryBaseDto
    {
        public int? Id { get; set; }

        public string GoogleUserId { get; set; }
    }
}
