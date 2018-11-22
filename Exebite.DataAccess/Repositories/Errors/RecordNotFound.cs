using Exebite.Common;

namespace Exebite.DataAccess.Repositories
{
    public class RecordNotFound : Error
    {
        public RecordNotFound(string message) => Message = message;

        public string Message { get; }
    }
}
