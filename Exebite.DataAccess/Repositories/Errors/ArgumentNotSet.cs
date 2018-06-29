namespace Exebite.DataAccess.Repositories
{
    public class ArgumentNotSet : Error
    {
        private string message;

        public ArgumentNotSet(string message)
        {
            this.message = message;
        }
    }
}