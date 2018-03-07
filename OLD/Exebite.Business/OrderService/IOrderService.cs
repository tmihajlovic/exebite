using Exebite.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exebite.Business
{
    public interface IOrderService
    {
        List<Order> GetAllOrders();
        Order GetOrderById(int orderId);
        List<Order> GetAllOrdersForCustomer(int customerId);
        List<Order> GetAllOrdersForRestoraunt(int restorauntId);
        List<Order> GettOrdersForDate(DateTime date);
        void PlaceOreder(Order order);
        void EditOrder(Order order);
        void CancelOrder(int orderId);
    }
}
