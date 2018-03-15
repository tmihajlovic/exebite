using System.Collections.Generic;
using Exebite.Model;

namespace Exebite.GoogleSheetAPI.Kasa
{
    public interface IKasaConector
    {
        List<Customer> GetCustomersFromKasa();
    }
}
