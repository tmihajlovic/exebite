namespace Exebite.DataAccess.Repositories
{
    public class PaymentQueryModel : QueryBase
    {
        public PaymentQueryModel()
            : base()
        {
        }

        public PaymentQueryModel(int page, int size)
            : base(page, size)
        {
        }

        public long? Id { get; set; }
    }
}