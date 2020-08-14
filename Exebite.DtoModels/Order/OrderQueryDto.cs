using System;

namespace Exebite.DtoModels
{
    public class OrderQueryDto : QueryBaseDto
    {
        public long? Id { get; set; }

        public DateTime? Date { get; set; }

        public long? CustomerId { get; set; }
    }
}
