using MuzickiFestivali.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace MuzickiFestivali.API.DTOs
{
    public class NastupDto
    {
        [Required(ErrorMessage = "Naziv je obavezan")]
        public string Naziv { get; set; }

        [Required(ErrorMessage = "Opis je obavezan")]
        public string Opis { get; set; }

        [Required(ErrorMessage = "Žanr je obavezan")]
        public Zanr Zanr { get; set; }
    }
}
