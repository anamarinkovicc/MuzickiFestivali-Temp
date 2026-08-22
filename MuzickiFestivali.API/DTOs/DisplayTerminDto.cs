namespace MuzickiFestivali.API.DTOs
{
    public class DisplayTerminDto
    {
        public int IdTermin { get; set; }
        public int IdNastup { get; set; }
        public int IdFestival { get; set; }
        public DateTime VremePocetka { get; set; }
        public DateTime VremeZavrsetka { get; set; }
        public string Tip { get; set; }
        public int IdBina { get; set; }
        public string? Napomena { get; set; }
    }
}
