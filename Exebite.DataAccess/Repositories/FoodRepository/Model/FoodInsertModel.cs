using Exebite.DomainModel;

namespace Exebite.DataAccess.Repositories
{
    public class FoodInsertModel
    {
        public string Name { get; set; }

        public MealType Type { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public bool IsInactive { get; set; }

        public int? DailyMenuId { get; set; }

        public int RestaurantId { get; set; }
    }
}