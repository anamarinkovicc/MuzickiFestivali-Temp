using MuzickiFestivali.Domain.Enums;

namespace MuzickiFestivali.API.DTOs
{
    public class DisplayNastupDto
    {
        public int IdNastup { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public Zanr Zanr { get; set; }
        public int IdFestival { get; set; }
    }
}
