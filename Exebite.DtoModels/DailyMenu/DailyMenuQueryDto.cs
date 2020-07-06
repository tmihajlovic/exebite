using System;

namespace Exebite.DtoModels
{
    public class DailyMenuQueryDto : QueryBaseDto
    {
        public long? Id { get; set; }

        public long? RestaurantId { get; set; }

        public DateTime? Date { get; set; }
    }
}