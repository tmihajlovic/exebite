using Exebite.Common;

namespace Exebite.DataAccess.Repositories
{
    public class ArgumentNotSet : Error
    {
        public ArgumentNotSet(string message) => Message = message;

        public string Message { get; }
    }
}