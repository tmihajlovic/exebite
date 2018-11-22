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

        public int? Id { get; set; }
    }
}