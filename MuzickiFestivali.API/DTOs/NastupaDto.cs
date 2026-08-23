using System.ComponentModel.DataAnnotations;

namespace MuzickiFestivali.API.DTOs
{
    public class NastupaDto
    {
        [Required(ErrorMessage = "Uloga izvođača je obavezna")]
        [StringLength(100, ErrorMessage = "Uloga ne može biti duža od 100 karaktera")]
        public string Uloga { get; set; }


        [StringLength(500, ErrorMessage = "Napomena ne može biti duža od 500 karaktera")]
        public string? Napomena { get; set; }
    }
}
