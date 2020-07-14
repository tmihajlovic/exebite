using System.ComponentModel.DataAnnotations;

namespace Exebite.DtoModels.GoogleLogin
{
    public class GoogleLoginDto
    {
        [Required]
        public string IdToken { get; set; }
    }
}
