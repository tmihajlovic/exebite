namespace Exebite.DataAccess.Repositories
{
    public interface ICustomerCommandRepository : IDatabaseCommandRepository<int, CustomerInsertModel, CustomerUpdateModel>
    {
    }
}
