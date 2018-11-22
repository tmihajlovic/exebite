using System;

namespace Exebite.GoogleSheetAPI.GoogleSSFactory
{
    public class GoogleSpreadsheetIdFactory : IGoogleSpreadsheetIdFactory
    {
        public string GetExtraFood()
        {
            throw new NotImplementedException();
        }

        public string GetHedone()
        {
            return Properties.Resources.Hedone;
        }

        public string GetIndexHouse()
        {
            throw new NotImplementedException();
        }

        public string GetLipa()
        {
            return Properties.Resources.Lipa;
        }

        public string GetTeglas()
        {
            return Properties.Resources.Teglas;
        }

        public string GetKasa()
        {
            return Properties.Resources.Kasa;
        }
    }
}
