using System;
using System.Linq;
using Exebite.DataAccess.Repositories;
using Exebite.GoogleSheetAPI.RestaurantConectorsInterfaces;

namespace Exebite.Business.GoogleApiImportExport
{
    public class GoogleApiExport : IGoogleDataExporter
    {
        // Services
        private readonly IOrderService _orderService;
        private readonly ICustomerRepository _customerRepository;
        private readonly IRestaurantRepository _restaurantRepository;

        // Conectors
        private readonly ILipaConector _lipaConector;
        private readonly IHedoneConector _hedoneConector;
        private readonly ITeglasConector _teglasConector;

        public GoogleApiExport(ITeglasConector teglasConector, IHedoneConector hedoneConector, ILipaConector lipaConector, IOrderService orderService, ICustomerRepository customerRepository, IRestaurantRepository restaurantRepository)
        {
            // Conectors
            _lipaConector = lipaConector;
            _hedoneConector = hedoneConector;
            _teglasConector = teglasConector;

            // Services
            _orderService = orderService;
            _customerRepository = customerRepository;
            _restaurantRepository = restaurantRepository;
        }

        //IRestaurantRepository RestaurantRepository => _restaurantRepository;

        /// <summary>
        /// Place orders for restaurant
        /// </summary>
        /// <param name="restaurantName">Name of restaurant</param>
        public void PlaceOrdersForRestaurant(string restaurantName)
        {
            switch (restaurantName)
            {
                case "Restoran pod Lipom":
                    var lipa = _restaurantRepository.Query(new RestaurantQueryModel { Name = restaurantName }).FirstOrDefault();
                    if (lipa != null)
                    {
                        var lipaOrders = _orderService.GetAllOrdersForRestoraunt(lipa.Id).Where(o => o.Date == DateTime.Today.Date).ToList();
                        _lipaConector.PlaceOrders(lipaOrders);
                    }

                    break;

                case "Hedone":
                    var hedone = _restaurantRepository.Query(new RestaurantQueryModel { Name = restaurantName }).FirstOrDefault();
                    if (hedone != null)
                    {
                        var hedoneOrders = _orderService.GetAllOrdersForRestoraunt(hedone.Id).Where(o => o.Date == DateTime.Today.Date).ToList();
                        _hedoneConector.PlaceOrders(hedoneOrders);
                    }

                    break;

                case "Teglas":
                    var teglas = _restaurantRepository.Query(new RestaurantQueryModel { Name = restaurantName }).FirstOrDefault();
                    if (teglas != null)
                    {
                        var teglasOrders = _orderService.GetAllOrdersForRestoraunt(teglas.Id).Where(o => o.Date == DateTime.Today.Date).ToList();
                        _teglasConector.PlaceOrders(teglasOrders);
                    }

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
            var customerList = _customerRepository.Get(0, int.MaxValue).ToList();
            _teglasConector.WriteKasaTab(customerList);

            _lipaConector.WriteKasaTab(customerList);

            _hedoneConector.WriteKasaTab(customerList);
        }
    }
}
