namespace Exebite.DataAccess.Repositories
{
    public class PaymentInsertModel
    {
        public long CustomerId { get; set; }

        public decimal Amount { get; set; }
    }
}