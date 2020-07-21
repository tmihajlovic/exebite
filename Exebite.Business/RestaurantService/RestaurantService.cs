using System;
using System.Collections.Generic;
using System.Linq;
using Either;
using Exebite.Common;
using Exebite.DataAccess.Repositories;
using Exebite.DomainModel;
using Exebite.GoogleSheetAPI.Services;

namespace Exebite.Business
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IMealQueryRepository _mealQuery;
        private readonly ICustomerQueryRepository _customerQuery;
        private readonly ILocationQueryRepository _locationQuery;
        private readonly IOrderCommandRepository _orderCommand;
        private readonly IGoogleSheetAPIService _apiService;

        public RestaurantService(
            IMealQueryRepository mealQuery,
            ICustomerQueryRepository customerQuery,
            ILocationQueryRepository locationQuery,
            IOrderCommandRepository orderCommand,
            IGoogleSheetAPIService googleSheetAPIService)
        {
            _mealQuery = mealQuery;
            _customerQuery = customerQuery;
            _locationQuery = locationQuery;
            _orderCommand = orderCommand;
            _apiService = googleSheetAPIService;
        }

        public Either<Error, long> PlaceOrdersForRestaurant(Order order)
        {
            List<Meal> meals = new List<Meal>();

            if (order == null)
            {
                return new Left<Error, long>(new UnknownError("Order is null"));
            }

            var customer = _customerQuery.Query(new CustomerQueryModel() { Id = order.Customer.Id })
                .Map(c => c.Items.ToList())
                .Reduce(_ => throw new Exception())
                .FirstOrDefault();
            var location = _locationQuery.Query(new LocationQueryModel() { Id = order.Location.Id })
                .Map(l => l.Items.ToList())
                .Reduce(_ => throw new Exception())
                .FirstOrDefault();
            customer.DefaultLocation = location;

            foreach (var mealOrder in order.OrdersToMeals)
            {
                var meal = _mealQuery.Query(new MealQueryModel() { Id = mealOrder.Meal.Id })
                .Map(c => c.Items.ToList())
                .Reduce(_ => throw new Exception());

                meals.Add(meal.FirstOrDefault());
            }

            var returnValue = _orderCommand.Insert(new OrderInsertModel()
            {
                CustomerId = order.Customer.Id,
                Date = order.Date,
                LocationId = order.Location.Id,
                Price = order.Price
            });

            _apiService.WriteOrder(customer, meals);

            return returnValue;
        }

        /// <summary>
        /// Update daily menu for restaurants
        /// </summary>
        public void UpdateRestaurantMenu()
        {
            _apiService.UpdateDailyMenuLipa();
            _apiService.UpdateDailyMenuMimas();
            _apiService.UpdateDailyMenuParrilla();
            _apiService.UpdateDailyMenuTopliObrok();
            _apiService.UpdateDailyMenuSerpica();
        }
    }
}
