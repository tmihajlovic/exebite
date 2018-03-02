using Exebite.Model;
using System.Collections.Generic;

namespace Exebite.Business.GoogleApiImportExport
{
    public interface IGoogleApiOldSheets
    {
        void UpdateDailyMenu();
        List<Order> GetHistoricalData();
        void WriteOrdersToSheets();
    }
}
