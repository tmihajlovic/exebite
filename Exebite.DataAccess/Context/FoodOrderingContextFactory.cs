namespace Exebite.DataAccess.Context
{
    public class FoodOrderingContextFactory : IFoodOrderingContextFactory
    {
        private readonly IExebiteDbContextOptionsFactory _optionsFactory;

        public FoodOrderingContextFactory(IExebiteDbContextOptionsFactory optionsFactory)
        {
            _optionsFactory = optionsFactory;
        }

        public FoodOrderingContext Create()
        {
            var options = _optionsFactory.Create();

            return new FoodOrderingContext(options);
        }
    }
}
