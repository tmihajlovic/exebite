namespace Exebite.DomainModel
{
    public class OrderQueryModel : QueryBase
    {
        public OrderQueryModel()
        {
        }

        public OrderQueryModel(int page, int size)
            : base(page, size)
        {
        }

        public int? Id { get; set; }
    }
}
