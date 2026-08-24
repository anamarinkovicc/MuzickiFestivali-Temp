using System.ComponentModel.DataAnnotations;

namespace MuzickiFestivali.API.DTOs
{
    public class FestivalDto
    {
        [Required(
            ErrorMessageResourceType = typeof(MuzickiFestivali.API.Resources.SharedResources),
            ErrorMessageResourceName = "Festival_NameRequired")]
        [StringLength(100,
            ErrorMessageResourceType = typeof(MuzickiFestivali.API.Resources.SharedResources),
            ErrorMessageResourceName = "Festival_NameMaxLength")]
        public string Naziv { get; set; }

        [Required(
            ErrorMessageResourceType = typeof(MuzickiFestivali.API.Resources.SharedResources),
            ErrorMessageResourceName = "Festival_DescriptionRequired")]
        public string Opis { get; set; } 

        [Required(
            ErrorMessageResourceType = typeof(MuzickiFestivali.API.Resources.SharedResources),
            ErrorMessageResourceName = "Festival_StartDateRequired")]
        public DateTime DatumPocetka { get; set; }

        [Required(
            ErrorMessageResourceType = typeof(MuzickiFestivali.API.Resources.SharedResources),
            ErrorMessageResourceName = "Festival_EndDateRequired")]
        public DateTime DatumZavrsetka { get; set; }

        [Range(1, 1000000,
            ErrorMessageResourceType = typeof(MuzickiFestivali.API.Resources.SharedResources),
            ErrorMessageResourceName = "Festival_CapacityRange")]
        public int Kapacitet { get; set; }
    }
}
