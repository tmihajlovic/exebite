using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleSpreadsheetApi.GoogleSSFactory
{
    public interface IGoogleSpreadsheetIdFactory
    {
        string GetHedone();
        string GetLipa();
        string GetIndexHouse();
        string GetExtraFood();
        string GetTeglas();
        string GetNewLipa();
    }
}
