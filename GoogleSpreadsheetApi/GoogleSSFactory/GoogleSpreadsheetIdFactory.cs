namespace Exebite.GoogleSpreadsheetApi.GoogleSSFactory
{
    public class GoogleSpreadsheetIdFactory : IGoogleSpreadsheetIdFactory
    {
        public string GetExtraFood()
        {
            
            return Properties.Settings.Default.ExtraFoodSpredsheetID;
        }

        public string GetHedone()
        {
            return Properties.Settings.Default.HedoneSpredsheetID;
        }

        public string GetIndexHouse()
        {
            return Properties.Settings.Default.IndexHauseSpredsheetID;
        }

        public string GetLipa()
        {
            return Properties.Settings.Default.LipaSpredsheetID;
        }

        public string GetTeglas()
        {
            return Properties.Settings.Default.TeglasSpredsheetID;
        }

        public string GetNewLipa()
        {
            return Properties.Settings.Default.LipaNovi;
        }

        public string GetNewTeglas()
        {
            return Properties.Settings.Default.TeglasNovi;
        }

        public string GetNewHedone()
        {
            return Properties.Settings.Default.HedoneNovi;
        }
    }
}
