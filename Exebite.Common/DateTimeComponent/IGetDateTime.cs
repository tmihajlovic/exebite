using System;

namespace Exebite.Common
{
    public interface IGetDateTime
    {
         DateTime Now();

         DateTime UtcNow();
        
    }
}
