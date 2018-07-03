namespace Exebite.DomainModel
{
    public class CustomerQueryModel : QueryBase
    {
        public CustomerQueryModel()
        {
        }

        public CustomerQueryModel(int page, int size)
            : base(page, size)
        {
        }

        public int? Id { get; set; }
    }
}
