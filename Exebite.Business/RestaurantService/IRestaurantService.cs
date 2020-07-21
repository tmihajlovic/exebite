using Either;
using Exebite.Common;
using Exebite.DomainModel;

namespace Exebite.Business
{
    public interface IRestaurantService
    {
        /// <summary>
        /// Place orders for restaurant
        /// </summary>
        /// <param name="order">Restaurant order</param>
        /// <returns>Id or an error</returns>
        Either<Error, long> PlaceOrdersForRestaurant(Order order);

        /// <summary>
        /// Update daily menus for all restaurants
        /// </summary>
        void UpdateRestaurantMenu();
    }
}
