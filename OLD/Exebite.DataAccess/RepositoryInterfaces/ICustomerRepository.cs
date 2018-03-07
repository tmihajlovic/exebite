using Exebite.Model;
namespace Exebite.DataAccess
{
    public interface ICustomerRepository : IDatabaseRepository<Customer>
    {
        // Add functions specific for ICustomerHandler
        Customer GetByName(string name);
    }
}
