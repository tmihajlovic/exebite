using Exebite.Model;
using System.Collections.Generic;

namespace Exebite.GoogleSheetAPI.Kasa
{
    public interface IKasaConector
    {
        List<Customer> GetCustomersFromKasa();
    }
}
