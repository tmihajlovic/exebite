using System;
using System.Collections.Generic;
using System.Text;

namespace Exebite.Sheets.API
{
    public interface ISheetsAPI
    {
        IEnumerable<RestaurantOffer> GetOffers(DateTime date);
    }
}
