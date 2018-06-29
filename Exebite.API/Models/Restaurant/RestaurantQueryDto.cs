using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exebite.API.Models
{
    public class RestaurantQueryDto
    {
        public RestaurantQueryDto(int page, int size)
        {
            Page = page;
            Size = size;
        }

        public int Page { get; }

        public int Size { get; }

        public int? Id { get; set; }

        public string Name { get; set; }
    }
}
