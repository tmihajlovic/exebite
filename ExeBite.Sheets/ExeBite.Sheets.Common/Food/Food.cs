namespace Exebite.Sheets.Common.Models
{
    public class Food
    {
        /// <summary>
        /// Food name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Food price
        /// </summary>
        public double Price { get; private set; }

        /// <summary>
        /// Restaurant name
        /// </summary>
        public string Restaurant { get; private set; }

        /// <summary>
        /// Subcategory of the food item.
        /// </summary>
        public Category Category { get; private set; }

        /// <summary>
        /// Standard constructor.
        /// Protected so that only child classes can be instantiated.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="price"></param>
        /// <param name="restaurant"></param>
        /// <param name="subcategory"></param>
        protected Food(string name, double price, string restaurant, Category scategory)
        {
            Name = name;
            Price = price;
            Restaurant = restaurant;
            Category = scategory;
        }
    }
}
