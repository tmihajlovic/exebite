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
                case ESheetOwner.INDEX_HOUSE:
                    return Properties.Resources.INDEX_HOUSE;
                case ESheetOwner.DE_PAPAJ:
                    return Properties.Resources.DE_PAPAJ;
                case ESheetOwner.MIMAS:
                    return Properties.Resources.MIMAS;
                case ESheetOwner.KASA:
                    return Properties.Resources.KASA;
                case ESheetOwner.HEDONE:
                    return Properties.Resources.HEDONE;
                case ESheetOwner.TEGLAS:
                    return Properties.Resources.TEGLAS;
                case ESheetOwner.SERPICA:
                    throw new NotImplementedException("Serpica has its own thing.");
                default:
                    throw new ArgumentException("Chosen sheet owner does not exist.");
            }
        }
    }
}
