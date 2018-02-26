using Exebite.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleSpreadsheetApi.Kasa
{
    public interface IKasaConector
    {
        List<Customer> GetCustomersFromKasa();
    }
}
