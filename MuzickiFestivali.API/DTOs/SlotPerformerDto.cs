namespace MuzickiFestivali.API.DTOs
{
    public class SlotPerformerDto
    {
        public int IdOsoba { get; set; }
        public string ImePrezime { get; set; } = string.Empty;
        public string? UmetnickoIme { get; set; }
        public string Uloga { get; set; } = string.Empty;
        public bool PotvrdjenDolazak { get; set; }
    }
}
