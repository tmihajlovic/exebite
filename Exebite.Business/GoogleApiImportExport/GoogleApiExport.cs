using System.Collections.Generic;
using Exebite.DataAccess;
using Exebite.Model;
using Exebite.GoogleSpreadsheetApi;
using Exebite.GoogleSpreadsheetApi.GoogleSSFactory;
using Exebite.GoogleSpreadsheetApi.Strategies;
using System.Linq;
using Exebite.GoogleSpreadsheetApi.RestaurantConectorsInterfaces;

namespace Exebite.Business
{
    public class GoogleApiExport : IGoogleDataExporter
    {
        IGoogleSheetServiceFactory _googleSheetServiceFactory;
        IGoogleSpreadsheetIdFactory _googleSpreadsheetIdFactory;
        IRestaurantRepository _restaurantHandler;
        ILipaConector _lipa;
        IHedoneConector _hedone;
        IRestaurantStrategy _indexHouse;
        ITeglasConector _teglas;
        IRestaurantStrategy _extraFood;

        public GoogleApiExport(IGoogleSheetServiceFactory googleSheetServiceFactory, IGoogleSpreadsheetIdFactory googleSpreadsheetIdFactory,
            IRestaurantRepository restaurantHandler, 
            ITeglasConector teglasConector, IHedoneConector hedoneConector, ILipaConector lipaConector)
        {
            _googleSheetServiceFactory = googleSheetServiceFactory;
            _googleSpreadsheetIdFactory = googleSpreadsheetIdFactory;

            _lipa = lipaConector;
            _hedone = hedoneConector;
            //_indexHouse = new IndexHouseStrategy(_googleSheetServiceFactory, _googleSpreadsheetIdFactory);
            _teglas = teglasConector;
            //_extraFood = new ExtraFoodStrategy(_googleSheetServiceFactory, _googleSpreadsheetIdFactory);
            _restaurantHandler = restaurantHandler;
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

            _lipa.PlaceOrders(lipaOrders);
            _hedone.PlaceOrders(hedoneOrders);
            _teglas.PlaceOrders(teglasOreder);
            
        }

    }
}
