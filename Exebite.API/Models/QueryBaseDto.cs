using System.ComponentModel.DataAnnotations;
using Exebite.DomainModel;

namespace Exebite.API.Models
{
    public abstract class QueryBaseDto
    {
        [Required]
        [Range(1, QueryConstants.MaxElements)]
        public int Size { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Page { get; set; }
    }
}
