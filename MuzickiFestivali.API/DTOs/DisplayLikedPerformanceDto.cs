namespace MuzickiFestivali.API.DTOs
{
    public class DisplayLikedPerformanceDto
    {
        public int IdNastup { get; set; }
        public int IdFestival { get; set; }
        public string NazivNastupa { get; set; }
        public string OpisNastupa { get; set; }
        public string Zanr { get; set; }
        public DateTime DatumVremeLajka { get; set; }
    }
}
