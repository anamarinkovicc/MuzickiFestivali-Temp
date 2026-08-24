using MuzickiFestivali.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace MuzickiFestivali.API.DTOs
{
    public class NastupDto
    {
        [Required(
            ErrorMessageResourceType = typeof(MuzickiFestivali.API.Resources.SharedResources),
            ErrorMessageResourceName = "Performance_NameRequired")]
        public string Naziv { get; set; }

        [Required(
            ErrorMessageResourceType = typeof(MuzickiFestivali.API.Resources.SharedResources),
            ErrorMessageResourceName = "Performance_DescriptionRequired")]
        public string Opis { get; set; }

        [Required(
            ErrorMessageResourceType = typeof(MuzickiFestivali.API.Resources.SharedResources),
            ErrorMessageResourceName = "Performance_GenreRequired")]
        public Zanr Zanr { get; set; }
    }
}
