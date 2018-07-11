namespace Exebite.DataAccess.Repositories
{
    public interface IPaymentCommandRepository : IDatabaseCommandRepository<int, PaymentInsertModel, PaymentUpdateModel>
    {
    }
}
