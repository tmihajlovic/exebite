namespace Exebite.Model
{
    public class Food
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public FoodType Type { get; set; }

        public decimal Price { get; set; }

        public Restaurant Restaurant { get; set; }

        public string Description { get; set; }

        public bool IsInactive { get; set; }
    }
}
