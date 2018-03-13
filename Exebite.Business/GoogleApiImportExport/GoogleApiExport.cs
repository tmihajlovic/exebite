using System.Collections.Generic;
using System.Linq;
using Exebite.DataAccess;
using Exebite.GoogleSpreadsheetApi.RestaurantConectorsInterfaces;
using Exebite.Model;

namespace Exebite.Business.GoogleApiImportExport
{
    public class GoogleApiExport : IGoogleDataExporter
    {
        // Services
        private IOrderService _orderService;
        private ICustomerService _customerService;

        // Conectors
        private ILipaConector _lipaConector;
        private IHedoneConector _hedoneConector;
        private ITeglasConector _teglasConector;

        public GoogleApiExport(ITeglasConector teglasConector, IHedoneConector hedoneConector, ILipaConector lipaConector, IOrderService orderService, ICustomerService customerService)
        {
            // Conectors
            _lipaConector = lipaConector;
            _hedoneConector = hedoneConector;
            _teglasConector = teglasConector;

            // Services
            _orderService = orderService;
            _customerService = customerService;
        }

        /// <summary>
        /// Place orders
        /// </summary>
        /// <param name="orderList">List of orders to place</param>
        /// <param name="restaurant">Restaurant to place orders to</param>
        public void PlaceOrders(List<Order> orderList, Restaurant restaurant)
        {
            switch (restaurant.Name)
            {
                case "Restoran pod Lipom":
                    _lipaConector.PlaceOrders(orderList);
                    break;

                case "Hedone":
                    _hedoneConector.PlaceOrders(orderList);
                    break;

                case "Teglas":
                    _teglasConector.PlaceOrders(orderList);
                    break;
            }
        }

        /// <summary>
        /// Rotates daily menu so today is in first column
        /// </summary>
        public void SetupDailyMenuDayOrder()
        {
            _lipaConector.DnevniMenuSheetSetup();
            _hedoneConector.DnevniMenuSheetSetup();
        }

        /// <summary>
        /// Updates tab "kasa"
        /// </summary>
        public void UpdateKasaTab()
        {
            var customerList = _customerService.GetAllCustomers();
            _teglasConector.WriteKasaTab(customerList);

            _lipaConector.WriteKasaTab(customerList);

            _hedoneConector.WriteKasaTab(customerList);
        }
    }
}
