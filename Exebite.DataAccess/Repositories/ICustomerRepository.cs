using Exebite.Model;

namespace Exebite.DataAccess.Repositories
{
    public interface ICustomerRepository : IDatabaseRepository<Customer>
    {
        /// <summary>
        /// Get <see cref="Customer"/> by name
        /// </summary>
        /// <param name="name">Name of <see cref="Customer"/></param>
        /// <returns><see cref="Customer"/> whith given name</returns>
        Customer GetByName(string name);
    }
}
