namespace Exebite.DataAccess.Repositories
{
    public interface IPaymentCommandRepository : IDatabaseCommandRepository<long, PaymentInsertModel, PaymentUpdateModel>
    {
    }
}
