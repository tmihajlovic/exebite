namespace Exebite.DataAccess.Context
{
    public class MealOrderingContextFactory : IMealOrderingContextFactory
    {
        private readonly IExebiteDbContextOptionsFactory _optionsFactory;

        public MealOrderingContextFactory(IExebiteDbContextOptionsFactory optionsFactory)
        {
            _optionsFactory = optionsFactory;
        }

        public MealOrderingContext Create()
        {
            var options = _optionsFactory.Create();

            return new MealOrderingContext(options);
        }
    }
}
