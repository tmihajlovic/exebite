namespace Exebite.Sheets.Common.Models
{
    /// <summary>
    /// Food that is found in Daily Offers
    /// </summary>
    public class DailyOfferFood : Food
    {
        #region Constructors
        /// <summary>
        /// Constructor that creates the daily food offer.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="price"></param>
        public DailyOfferFood(string name, double price, string restaurant, Category category)
            : base(name, price, restaurant, category) {}
        #endregion

        #region Public methods
        /// <summary>
        /// Override of ToString method.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Name} - {Price}";
        } 
        #endregion
    }
}
