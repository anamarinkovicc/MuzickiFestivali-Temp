namespace MuzickiFestivali.API.DTOs
{
    public class DisplayNastupaDto
    {
        public int IdOsoba { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string UmetnickoIme { get; set; }
        public string Uloga { get; set; }
        public bool PotvrdjenDolazak { get; set; }
        public string? Napomena { get; set; }
    }
}
