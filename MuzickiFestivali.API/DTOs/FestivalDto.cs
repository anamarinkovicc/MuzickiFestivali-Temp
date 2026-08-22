using System.ComponentModel.DataAnnotations;

namespace MuzickiFestivali.API.DTOs
{
    public class FestivalDto
    {
        [Required(ErrorMessage = "Naziv festivala je obavezan")]
        [StringLength(100, ErrorMessage = "Naziv ne može biti duži od 100 karaktera")]
        public string Naziv { get; set; }

        [Required(ErrorMessage = "Opis je obavezan")]
        public string Opis { get; set; }

        [Required]
        public DateTime DatumPocetka { get; set; }

        [Required]
        public DateTime DatumZavrsetka { get; set; }

        [Range(1, 1000000, ErrorMessage = "Kapacitet mora biti pozitivan broj")]
        public int Kapacitet { get; set; }
    }
}
