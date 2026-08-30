namespace MuzickiFestivali.API.DTOs
{
    public class DisplayUserDto
    {
        public int UserId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public string Uloga { get; set; } 
        public string? Pozicija { get; set; } 
        public string? UmetnickoIme { get; set; } 
    }
}
