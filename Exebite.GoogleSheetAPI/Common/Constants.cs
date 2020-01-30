namespace Exebite.GoogleSheetAPI.Common
{
    public class Constants
    {
        /// <summary>
        /// App name used to identify this app to google servers.
        /// </summary>
        public const string APP_NAME = "Exebite Sheet Overseer";

        /// <summary>
        /// Name of Hedone restaurant
        /// </summary>
        public const string HEDONE_NAME = "Hedone";

        /// <summary>
        /// Name of the Index House restaurant
        /// </summary>
        public const string INDEX_NAME = "Index House";

        /// <summary>
        /// Name of the Pod Lipom Restaurant
        /// </summary>
        public const string POD_LIPOM_NAME = "Restoran pod Lipom";

        /// <summary>
        /// Name of the Teglas restaurant
        /// </summary>
        public const string TEGLAS_NAME = "Teglas";

        /// <summary>
        /// Category is not specified.
        /// </summary>
        public const string CATEGORY_NONE = "Unspecified";

        /// <summary>
        /// Offer is standard everyday offer
        /// </summary>
        public const string CATEGORY_STANDARD = "Standard Offer";

        /// <summary>
        /// Offer is in the daily offer category.
        /// </summary>
        public const string CATEGORY_DAILY = "Daily Offer";

        /// <summary>
        /// Sleep time between two requests, in miliseconds.
        /// </summary>
        public const int SLEEP_TIME = 50;
    }
}

