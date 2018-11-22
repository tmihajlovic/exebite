using System.Collections.Generic;
using Exebite.DomainModel;

namespace Exebite.GoogleSheetAPI.Kasa
{
    public interface IKasaConector
    {
        List<Customer> GetCustomersFromKasa();
    }
}
