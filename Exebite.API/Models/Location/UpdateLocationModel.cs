namespace Exebite.API.Models
{
    public class UpdateLocationModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }
    }
}
