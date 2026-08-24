using System.ComponentModel.DataAnnotations;

namespace MuzickiFestivali.API.DTOs
{
    public class LoginDto
    {
        [Required(
           ErrorMessageResourceType = typeof(MuzickiFestivali.API.Resources.SharedResources),
           ErrorMessageResourceName = "User_EmailRequired")]
        [EmailAddress(
           ErrorMessageResourceType = typeof(MuzickiFestivali.API.Resources.SharedResources),
           ErrorMessageResourceName = "User_EmailInvalid")]
        public string Email { get; set; }

        [Required(
            ErrorMessageResourceType = typeof(MuzickiFestivali.API.Resources.SharedResources),
            ErrorMessageResourceName = "User_PasswordRequired")]
        public string Lozinka { get; set; }
    }
}
