using System.Collections.Generic;
using Exebite.DomainModel;

namespace Exebite.GoogleSheetAPI.Connectors.Kasa
{
    public interface IKasaConnector
    {
        /// <summary>
        /// Retrieve all application Customers. This is currently only reliable from Kasa tab.
        /// </summary>
        /// <returns>List of Customers.</returns>
        IEnumerable<Customer> GetAllCustomers();
    }
}