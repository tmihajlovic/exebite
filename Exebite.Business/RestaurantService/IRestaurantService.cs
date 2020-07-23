using Either;
using Exebite.Business.Model;
using Exebite.Common;

namespace Exebite.Business
{
    public interface IRestaurantService
    {
        /// <summary>
        /// Place orders for restaurant
        /// </summary>
        /// <param name="order">Restaurant order</param>
        /// <returns>Id or an error</returns>
        Either<Error, RestaurantOrder> PlaceOrdersForRestaurant(RestaurantOrder order);

        /// <summary>
        /// Update daily menus for all restaurants
        /// </summary>
        void UpdateRestaurantMenu();
    }
}
