using System;
using System.Collections.Generic;
using System.Text;

namespace Exebite.DataAccess.Repositories
{
    public class UnknownError : Error
    {
        public string Message { get; }

        public UnknownError(string message)
        {
            Message = message;
        }
    }
}
