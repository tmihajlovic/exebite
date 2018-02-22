using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace GoogleSpreadsheetApi.Test
{
    public static class UnityConfig
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            GoogleSpreadsheetApi.Unity.UnityConfig.RegisterTypes(container);
        }
    }
}
