using System.Collections.Generic;
using Exebite.DataAccess;
using Exebite.Model;
using System.Linq;
using Exebite.GoogleSpreadsheetApi.RestaurantConectorsInterfaces;

namespace Exebite.Business.GoogleApiImportExport
{
    public class GoogleApiExport : IGoogleDataExporter
    {

        //repositoies
        IRestaurantRepository _restaurantRepository;
        //services
        IOrderService _orderService;
        ICustomerService _customerService;
        //conectors
        ILipaConector _lipaConector;
        IHedoneConector _hedoneConector;
        ITeglasConector _teglasConector;

        public GoogleApiExport(IRestaurantRepository restaurantRepository, ITeglasConector teglasConector, IHedoneConector hedoneConector, ILipaConector lipaConector, IOrderService orderService, ICustomerService customerService)
        {
            //conectors
            _lipaConector = lipaConector;
            _hedoneConector = hedoneConector;
            _teglasConector = teglasConector;
            //services
            _restaurantRepository = restaurantRepository;
            _orderService = orderService;
            _customerService = customerService;
        }
        /// <summary>
        /// Place orders
        /// </summary>
        /// <param name="orderList">List of orders to place</param>
        public void PlaceOrders(List<Order> orderList)
        {
            List<Order> teglasOreder = orderList.Where(o => o.Meal.Foods[0].Restaurant.Id == 4).ToList();
            List<Order> lipaOrders = orderList.Where(o => o.Meal.Foods[0].Restaurant.Id == 1).ToList();
            List<Order> hedoneOrders = orderList.Where(o => o.Meal.Foods[0].Restaurant.Id == 2).ToList();

            _lipaConector.PlaceOrders(lipaOrders);
            _hedoneConector.PlaceOrders(hedoneOrders);
            _teglasConector.PlaceOrders(teglasOreder);
            
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
