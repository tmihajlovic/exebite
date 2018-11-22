﻿using System;

namespace Exebite.DtoModels
{
    public class OrderQueryDto : QueryBaseDto
    {
        public int? Id { get; set; }

        // TODO: change this to be our class with data only
        public DateTime? Date { get; set; }

        public int? CustomerId { get; set; }
    }
}
