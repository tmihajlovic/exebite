namespace Exebite.Common
{
    public class MappingError : Error
    {
        public MappingError(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}
