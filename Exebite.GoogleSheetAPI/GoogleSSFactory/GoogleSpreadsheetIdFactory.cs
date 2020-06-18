using System;
using Exebite.GoogleSheetAPI.Enums;

namespace Exebite.GoogleSheetAPI.GoogleSSFactory
{
    public class GoogleSpreadsheetIdFactory : IGoogleSpreadsheetIdFactory
    {
        public string GetSheetId(ESheetOwner sheetOwner)
        {
            switch (sheetOwner)
            {
                case ESheetOwner.LIPA:
                    return Properties.Resources.LIPA;
                case ESheetOwner.TOPLI_OBROK:
                    return Properties.Resources.TOPLI_OBROK;
                case ESheetOwner.INDEX_HOUSE:
                    return Properties.Resources.INDEX_HOUSE;
                case ESheetOwner.MIMAS:
                    return Properties.Resources.MIMAS;
                case ESheetOwner.KASA:
                    return Properties.Resources.KASA;
                case ESheetOwner.SERPICA:
                    return Properties.Resources.SERPICA;
                case ESheetOwner.HEY_DAY:
                    return Properties.Resources.HEY_DAY;
                case ESheetOwner.PARRILLA:
                    return Properties.Resources.PARRILLA;
                default:
                    throw new ArgumentException("Chosen sheet owner does not exist.");
            }
        }
    }
}
