namespace Exebite.DataAccess.Repositories
{
    public sealed class FutureDate : Error
    {
        public FutureDate(string message) => Message = message;

        public string Message { get; }
    }
}
