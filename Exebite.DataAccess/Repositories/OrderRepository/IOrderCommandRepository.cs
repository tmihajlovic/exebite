namespace Exebite.DataAccess.Repositories
{
    public interface IOrderCommandRepository : IDatabaseCommandRepository<long, OrderInsertModel, OrderUpdateModel>
    {
    }
}
