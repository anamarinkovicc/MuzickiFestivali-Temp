using System.ComponentModel.DataAnnotations;

namespace MuzickiFestivali.API.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email je obavezan")]
        [EmailAddress(ErrorMessage = "Neispravan format email adrese")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Lozinka je obavezna")]
        public string Lozinka { get; set; }
    }
}
