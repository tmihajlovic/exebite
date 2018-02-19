using Exebite.Model;
namespace Exebite.DataAccess.Customers
{
    public interface ICustomerHandler : IDatabaseHandler<Customer>
    {
        // Add functions specific for ICustomerHandler
        Customer GetByName(string name);
    }
}
