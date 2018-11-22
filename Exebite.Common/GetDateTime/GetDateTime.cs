using System;

namespace Exebite.Common
{
    public class GetDateTime : IGetDateTime
    {
        public DateTime Now()
        {
            return DateTime.Now;
        }

        public DateTime UtcNow()
        {
            return DateTime.UtcNow;
        }
    }
}
