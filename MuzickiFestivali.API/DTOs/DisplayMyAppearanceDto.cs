namespace MuzickiFestivali.API.DTOs
{
    public class DisplayMyAppearanceDto
    {
        public int IdFestival { get; set; }
        public string FestivalNaziv { get; set; }

        public int IdNastup { get; set; }
        public string NastupNaziv { get; set; }

        public int IdTermin { get; set; }
        public DateTime VremePocetka { get; set; }
        public DateTime VremeZavrsetka { get; set; }
        public string TipTermina { get; set; }

        public string BinaNaziv { get; set; }

        public string Uloga { get; set; }
        public bool PotvrdjenDolazak { get; set; }
        public string? OrganizatorNapomena { get; set; }
    }
}
