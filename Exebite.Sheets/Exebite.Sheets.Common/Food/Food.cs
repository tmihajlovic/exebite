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
        /// <param name="name">Name of the food</param>
        /// <param name="price">Food price in RSD</param>
        /// <param name="restaurant">Restaurant name</param>
        /// <param name="category">Category and subcategory of the food</param>
        protected Food(string name, double price, string restaurant, Category category)
        {
            Name = name;
            Price = price;
            Restaurant = restaurant;
            Category = category;
        }
    }
}
