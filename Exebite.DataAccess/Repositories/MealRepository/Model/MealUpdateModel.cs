using Exebite.DomainModel;

namespace Exebite.DataAccess.Repositories
{
    public class MealUpdateModel
    {
        public string Name { get; set; }

        public MealType Type { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string Note { get; set; }

        public bool IsActive { get; set; }

        public int RestaurantId { get; set; }
    }
}