namespace ExeBite.Sheets.Common
{
    /// <summary>
    /// Food that is found in Daily Offers
    /// </summary>
    public class DailyOfferFood : Food
    {
        private const string DailyOfferCategory = "Daily Offer";
        #region Constructors
        /// <summary>
        /// Constructor that creates the daily food offer.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="price"></param>
        public DailyOfferFood(string name, double price, string restaurant)
            : base(name, price, restaurant, DailyOfferCategory) {}
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
