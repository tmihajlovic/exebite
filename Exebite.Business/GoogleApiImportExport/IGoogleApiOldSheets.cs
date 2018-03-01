using Exebite.Model;
using System.Collections.Generic;

namespace Exebite.Business.GoogleApiImportExport
{
    public interface IGoogleApiOldSheets
    {
        List<Order> GetHistoricalData();
        void WriteOrdersToSheets(List<Order> orders);
    }
}
