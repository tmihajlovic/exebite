using Exebite.Business;
using Exebite.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gLogin.Controllers
{
    public class OrderController : Controller
    {
        IOrderService _orderService;
        //ctor
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: Order
        public ActionResult Orders()
        {
            var todayOrders = _orderService.GettOrdersForDate(DateTime.Today);

            return View(todayOrders);
        }

        public ActionResult CancelOrder(Order order)
        {
            _orderService.CancelOrder(order.Id);
            return RedirectToAction("Orders");
        }
    }
}