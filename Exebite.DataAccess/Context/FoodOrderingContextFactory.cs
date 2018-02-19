namespace Exebite.DataAccess.Context
{
    public class FoodOrderingContextFactory : IFoodOrderingContextFactory
    {
        public FoodOrderingContext Create()
        {
            return new FoodOrderingContext();
        }
    }
}
