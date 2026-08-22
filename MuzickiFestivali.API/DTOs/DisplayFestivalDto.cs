namespace MuzickiFestivali.API.DTOs
{
    public class DisplayFestivalDto
    {
        public int IdFestival { get; set; } 
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public DateTime DatumPocetka { get; set; }
        public DateTime DatumZavrsetka { get; set; }
        public int Kapacitet { get; set; }

        public bool JeUToku => DateTime.Now >= DatumPocetka && DateTime.Now <= DatumZavrsetka;
    }
}
