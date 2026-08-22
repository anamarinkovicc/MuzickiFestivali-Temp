using System.ComponentModel.DataAnnotations;

namespace MuzickiFestivali.API.DTOs
{
    public class RegisterZaposleniDto
    {
        [Required(ErrorMessage = "Ime je obavezno")]
        public string Ime { get; set; }

        [Required(ErrorMessage = "Prezime je obavezno")]
        public string Prezime { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Neispravan format email adrese")]
        public string Email { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Lozinka mora imati bar 6 karaktera")]
        public string Lozinka { get; set; }

        [Required(ErrorMessage = "Pozicija je obavezna")]
        public string Pozicija { get; set; }
    }
}
