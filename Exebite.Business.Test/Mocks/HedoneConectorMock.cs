using System;
using System.Collections.Generic;
using Exebite.GoogleSheetAPI.RestaurantConectorsInterfaces;
using Exebite.Model;

namespace Exebite.Business.Test.Mocks
{
    public class HedoneConectorMock : IHedoneConector
    {
        public void DnevniMenuSheetSetup()
        {
        }

        public List<Food> GetDailyMenu()
        {
            return new List<Food>();
        }

        public List<Food> LoadAllFoods()
        {
            return new List<Food>();
        }

        public void PlaceOrders(List<Order> orders)
        {
            throw new NotImplementedException();
        }

        public void WriteKasaTab(List<Customer> customerList)
        {
        }

        public void WriteMenu(List<Food> foods)
        {
        }
    }
}
