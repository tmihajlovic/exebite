using Exebite.Model;
using System.Collections.Generic;

namespace Exebite.Business.GoogleApiImportExport
{
    public interface IGoogleDataExporter
    {

        void PlaceOrders(List<Order> orderList);
        void SetupDailyMenuDayOrder();
        void UpdateKasaTab();
    }
}
