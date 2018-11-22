namespace Exebite.DataAccess.Repositories
{
    public interface IOrderCommandRepository : IDatabaseCommandRepository<int, OrderInsertModel, OrderUpdateModel>
    {
    }
}
