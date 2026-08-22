using MuzickiFestivali.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace MuzickiFestivali.API.DTOs
{
    public class TerminDto
    {
        [Required(ErrorMessage = "Vreme početka je obavezno")]
        public DateTime VremePocetka { get; set; }

        [Required(ErrorMessage = "Vreme završetka je obavezno")]
        public DateTime VremeZavrsetka { get; set; }

        public string? Napomena { get; set; }

        [Required(ErrorMessage = "Tip termina je obavezan")]
        public TipTermina Tip { get; set; }

        [Required(ErrorMessage = "Morate odabrati binu za ovaj termin")]
        public int IdBina { get; set; }
    }
}
