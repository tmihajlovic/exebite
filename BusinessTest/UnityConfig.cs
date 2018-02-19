using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace BusinessTest
{
    public static class UnityConfig
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            GoogleSpreadsheetApi.UnityConfig.RegisterTypes(container);
            Exebite.Business.UnityConfig.RegisterTypes(container);
        }
    }
}
