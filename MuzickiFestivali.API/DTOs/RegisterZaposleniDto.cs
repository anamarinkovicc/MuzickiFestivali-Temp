using System.ComponentModel.DataAnnotations;

namespace MuzickiFestivali.API.DTOs
{
    public class RegisterZaposleniDto
    {
        [Required(
            ErrorMessageResourceType = typeof(MuzickiFestivali.API.Resources.SharedResources),
            ErrorMessageResourceName = "User_FirstNameRequired")]
        public string Ime { get; set; }

        [Required(
            ErrorMessageResourceType = typeof(MuzickiFestivali.API.Resources.SharedResources),
            ErrorMessageResourceName = "User_LastNameRequired")]
        public string Prezime { get; set; }

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
        [MinLength(6,
            ErrorMessageResourceType = typeof(MuzickiFestivali.API.Resources.SharedResources),
            ErrorMessageResourceName = "User_PasswordMinLength")]
        public string Lozinka { get; set; }

        [Required(
            ErrorMessageResourceType = typeof(MuzickiFestivali.API.Resources.SharedResources),
            ErrorMessageResourceName = "Employee_PositionRequired")]
        public string Pozicija { get; set; }
    }
}
