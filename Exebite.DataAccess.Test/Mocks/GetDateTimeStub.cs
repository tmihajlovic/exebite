using System;
using Exebite.Common;

namespace Exebite.DataAccess.Test.Mocks
{
    internal sealed class GetDateTimeStub : IGetDateTime
    {
        private readonly DateTime _now;

        public GetDateTimeStub()
        {
            _now = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        }

        public DateTime Now()
        {
            return _now.ToLocalTime();
        }

        public DateTime UtcNow()
        {
            return _now.ToUniversalTime();
        }
    }
}
