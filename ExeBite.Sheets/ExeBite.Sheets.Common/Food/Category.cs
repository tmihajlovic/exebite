namespace Exebite.Sheets.Common
{
    public class Category
    {
        #region Public properties
        /// <summary>
        /// Denotes food group,
        /// ie: Standard offer, Daily offer, Meal part, etc
        /// </summary>
        public string FoodGroup { get; private set; }

        /// <summary>
        /// Denotes food type
        /// ie: Burger, Dessert, Chicken Nuggets, etc 
        /// </summary>
        public string FoodCategory { get; private set; } 
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor, sets everything as not specified.
        /// </summary>
        public Category()
        {
            FoodGroup = Constants.CATEGORY_NONE;
            FoodCategory = Constants.CATEGORY_NONE;
        }

        /// <summary>
        /// Constructor that only defines Good group.
        /// </summary>
        /// <param name="group"></param>
        public Category(string group)
            : this()
        {
            FoodGroup = group;
        }

        /// <summary>
        /// Standard Constructor, initializes all fields.
        /// </summary>
        /// <param name="group"></param>
        /// <param name="category"></param>
        public Category(string group, string category)
            : this(group)
        {
            FoodCategory = category;
        }
        #endregion
    }
}
