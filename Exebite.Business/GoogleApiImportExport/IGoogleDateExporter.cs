using Exebite.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exebite.Business
{
    interface IGoogleDateExporter
    {

        void PlaceOrders(List<Order> orderList);
    }
}
