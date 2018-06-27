namespace Exebite.DataAccess.Repositories
{
    internal class ArgumentNotSet : Error
    {
        private string message;

        public ArgumentNotSet(string message)
        {
            this.message = message;
        }
    }
}