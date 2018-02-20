using Exebite.Model;
namespace Exebite.DataAccess
{
    public interface ICustomerHandler : IDatabaseHandler<Customer>
    {
        // Add functions specific for ICustomerHandler
        Customer GetByName(string name);
    }
}
