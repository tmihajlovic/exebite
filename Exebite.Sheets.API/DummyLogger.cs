using Exebite.Sheets.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exebite.Sheets.API
{
    class DummyLogger : ILogger
    {
        public void LogError(string text)
        {
            //Nothing.
        }

        public void LogInfo(string text)
        {
            //Nothing.
        }

        public void LogWarning(string text)
        {
            //Nothing.
        }
    }
}
