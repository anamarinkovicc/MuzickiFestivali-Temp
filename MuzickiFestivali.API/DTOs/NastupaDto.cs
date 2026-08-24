using System.ComponentModel.DataAnnotations;

namespace MuzickiFestivali.API.DTOs
{
    public class NastupaDto
    {
        [Required(
            ErrorMessageResourceType = typeof(MuzickiFestivali.API.Resources.SharedResources),
            ErrorMessageResourceName = "Appearance_RoleRequired")]
        [StringLength(100,
            ErrorMessageResourceType = typeof(MuzickiFestivali.API.Resources.SharedResources),
            ErrorMessageResourceName = "Appearance_RoleMaxLength")]
        public string Uloga { get; set; }

        [StringLength(500,
            ErrorMessageResourceType = typeof(MuzickiFestivali.API.Resources.SharedResources),
            ErrorMessageResourceName = "Appearance_NoteMaxLength")]
        public string? Napomena { get; set; }
    }
}
