namespace Exebite.Sheets.Common
{
    public class Configuration
    {
        #region private members
        private const string credentialsLocation = @"D:\Exebite\Credentials\credentials.json";
        private const string tokenLocation = @"D:\Exebite\Credentials\token.json";

        private const string hedoneSheetID = "1BONWz-sNnFbXVK-0g8DPtJbkxPsgiV-ShaegHR0tHA4";
        private const string hedoneOfferRange = "'Opis jela sa cenama'!A3:D74";

        private const string podLipomSheetID = "1gBmFxn2zL27h9nH9eRnKPRcqAKG4KrU1kCWDZohbnSQ";

        private const string teglasSheetID = "13ZjoABX3tJdi4x5rUasMOVSLEbHuRc46CDzjP9MDLlI";
        private const string teglasOfferRange = "'Opisi salata i cene'!A2:D37";

        private const string indexSheetID = "1YXpUAA968i57rtLqKx2CilpbYlpXlxLDvnDXebuKXi8";
        private const string indexOfferRange = "'Opisi jela i cene'!A3:D100";
        #endregion

        #region public properties
        /// <summary>
        /// Location of the credentials JSON in the system.
        /// Used to authenticate the application to google services
        /// </summary>
        public static string CredentialsLocation
        {
            get { return credentialsLocation; }
        }

        /// <summary>
        /// Location where authorization token will be placed, after credentials have been used.
        /// Typically expires after 1 hour.
        /// </summary>
        public static string TokenLocation
        {
            get { return tokenLocation; }
        }

        /// <summary>
        /// Provides ID of the Hedone restaurant sheet 
        /// </summary>
        public static string HedoneSheetID
        {
            get { return hedoneSheetID; }
        }

        /// <summary>
        /// Provides range of food offering for every day in Hedone restaurant
        /// </summary>
        public static string HedoneOfferRange
        {
            get { return hedoneOfferRange; }
        }

        /// <summary>
        /// Provides ID of the Pod Lipom restaurant sheet
        /// </summary>
        public static string PodLipomSheetID
        {
            get { return podLipomSheetID; }
        }

        /// <summary>
        /// Provides ID of the Teglas restaurant sheet
        /// </summary>
        public static string TeglasSheetID
        {
            get { return teglasSheetID; }
        }

        /// <summary>
        /// Provides range of food offering for every day in Teglas restaurant
        /// </summary>
        public static string TeglasOfferRange
        {
            get { return teglasOfferRange; }
        }

        /// <summary>
        /// Provides ID of the Index restaurant sheet
        /// </summary>
        public static string IndexSheetID
        {
            get { return indexSheetID; }
        }

        /// <summary>
        /// Provides range of food offering for every day in Index restaurant
        /// </summary>
        public static string IndexOfferRange
        {
            get { return indexOfferRange; }
        }
        #endregion
    }
}
