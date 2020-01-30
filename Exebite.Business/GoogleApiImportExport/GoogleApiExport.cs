using System;
using System.Linq;
using Either;
using Exebite.DataAccess.Repositories;
using Exebite.GoogleSheetAPI.Connectors.Restaurants;

namespace Exebite.Business.GoogleApiImportExport
{
    public class GoogleApiExport : IGoogleDataExporter
    {
        // Services
        private readonly IOrderQueryRepository _orderQueryRepo;
        private readonly ICustomerQueryRepository _customerQueryRepo;
        private readonly IRestaurantQueryRepository _restaurantQueryRepo;

        // Connectors
        private readonly ILipaConnector _lipaConector;

        public GoogleApiExport(
            ILipaConnector lipaConector,
            IOrderQueryRepository orderQueryRepo,
            ICustomerQueryRepository customerQueryRepo,
            IRestaurantQueryRepository restaurantQueryRepo)
        {
            // Connectors
            _lipaConector = lipaConector;

            // Services
            _orderQueryRepo = orderQueryRepo;
            _customerQueryRepo = customerQueryRepo;
            _restaurantQueryRepo = restaurantQueryRepo;
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
                    var lipa = _restaurantQueryRepo.Query(new RestaurantQueryModel { Name = restaurantName })
                                                         .Map(x => x.Items.FirstOrDefault())
                                                         .Reduce(_ => throw new Exception());
                    if (lipa != null)
                    {
                        var lipaOrders = _orderQueryRepo.GetAllOrdersForRestaurant(lipa.Id, 1, int.MaxValue)
                                                        .Map(x => x.Items.Where(o => o.Date == DateTime.Today.Date).ToList())
                                                        .Reduce(_ => throw new Exception());
                        _lipaConector.PlaceOrders(lipaOrders);
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
        }

        /// <summary>
        /// Updates tab "kasa"
        /// </summary>
        public void UpdateKasaTab()
        {
            var customerList = _customerQueryRepo.Query(new CustomerQueryModel())
                                                 .Map(x => x.Items.ToList())
                                                 .Reduce(_ => throw new Exception());

            _lipaConector.WriteKasaTab(customerList);
        }
    }
}
