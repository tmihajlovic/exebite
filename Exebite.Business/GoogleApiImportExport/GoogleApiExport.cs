using System;
using System.Linq;
using Exebite.GoogleSheetAPI.RestaurantConectorsInterfaces;

namespace Exebite.Business.GoogleApiImportExport
{
    public class GoogleApiExport : IGoogleDataExporter
    {
        // Services
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;
        private readonly IRestaurantService _restaurantService;

        // Conectors
        private readonly ILipaConector _lipaConector;
        private readonly IHedoneConector _hedoneConector;
        private readonly ITeglasConector _teglasConector;

        public GoogleApiExport(ITeglasConector teglasConector, IHedoneConector hedoneConector, ILipaConector lipaConector, IOrderService orderService, ICustomerService customerService, IRestaurantService restaurantService)
        {
            // Conectors
            _lipaConector = lipaConector;
            _hedoneConector = hedoneConector;
            _teglasConector = teglasConector;

            // Services
            _orderService = orderService;
            _customerService = customerService;
            _restaurantService = restaurantService;
        }

        /// <summary>
        /// Place orders for restaurant
        /// </summary>
        /// <param name="restaurantName">Name of restaurant</param>
        public void PlaceOrdersForRestaurant(string restaurantName)
        {
            switch (restaurantName)
            {
                case "Restoran pod Lipom":
                    var lipa = _restaurantService.GetRestaurantByName(restaurantName);
                    var lipaOrders = _orderService.GetAllOrdersForRestoraunt(lipa.Id).Where(o => o.Date == DateTime.Today.Date).ToList();
                    _lipaConector.PlaceOrders(lipaOrders);
                    break;

                case "Hedone":
                    var hedone = _restaurantService.GetRestaurantByName(restaurantName);
                    var hedoneOrders = _orderService.GetAllOrdersForRestoraunt(hedone.Id).Where(o => o.Date == DateTime.Today.Date).ToList();
                    _hedoneConector.PlaceOrders(hedoneOrders);
                    break;

                case "Teglas":
                    var teglas = _restaurantService.GetRestaurantByName(restaurantName);
                    var teglasOrders = _orderService.GetAllOrdersForRestoraunt(teglas.Id).Where(o => o.Date == DateTime.Today.Date).ToList();
                    _teglasConector.PlaceOrders(teglasOrders);
                    break;

                default:
                    throw new ArgumentException("Invalid restaurant");
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
