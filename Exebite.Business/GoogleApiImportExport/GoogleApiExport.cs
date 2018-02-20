using System.Collections.Generic;
using Exebite.DataAccess;
using Exebite.Model;
using GoogleSpreadsheetApi;
using GoogleSpreadsheetApi.GoogleSSFactory;
using GoogleSpreadsheetApi.Strategies;

namespace Exebite.Business
{
    public class GoogleApiExport : IGoogleDateExporter
    {
        IGoogleSheetServiceFactory _googleSheetServiceFactory;
        IGoogleSpreadsheetIdFactory _googleSpreadsheetIdFactory;
        IRestaurantHandler _restaurantHandler;
        IRestaurantStrategy _lipa;
        IRestaurantStrategy _hedone;
        IRestaurantStrategy _indexHouse;
        IRestaurantStrategy _teglas;
        IRestaurantStrategy _extraFood;

        public GoogleApiExport(IGoogleSheetServiceFactory googleSheetServiceFactory, IGoogleSpreadsheetIdFactory googleSpreadsheetIdFactory, IRestaurantHandler restaurantHandler)
        {
            _googleSheetServiceFactory = googleSheetServiceFactory;
            _googleSpreadsheetIdFactory = googleSpreadsheetIdFactory;

            _lipa = new LipaStrategy(_googleSheetServiceFactory, _googleSpreadsheetIdFactory);
            _hedone = new HedoneStrategy(_googleSheetServiceFactory, _googleSpreadsheetIdFactory);
            _indexHouse = new IndexHouseStrategy(_googleSheetServiceFactory, _googleSpreadsheetIdFactory);
            _teglas = new TeglasStrategy(_googleSheetServiceFactory, _googleSpreadsheetIdFactory);
            _extraFood = new ExtraFoodStrategy(_googleSheetServiceFactory, _googleSpreadsheetIdFactory);
            _restaurantHandler = restaurantHandler;
        }
        /// <summary>
        /// Place orders
        /// </summary>
        /// <param name="orderList">List of orders to place</param>
        public void PlaceOrders(List<Order> orderList)
        {
            foreach (var order in orderList)
            {
                switch (order.Meal.Foods[0].Restaurant.Name)
                {
                    case "Restoran pod Lipom":
                        _lipa.PlaceOrder(order);
                        break;
                    case "Teglas":
                        _teglas.PlaceOrder(order);
                        break;
                    case "Index House":
                        _indexHouse.PlaceOrder(order);
                        break;
                    case "Hedone":
                        _hedone.PlaceOrder(order);
                        break;
                    case "Extra food":
                        _extraFood.PlaceOrder(order);
                        break;
                    default:
                        break;
                }
            }
        }

    }
}
