using System;
using System.Collections.Generic;
using System.Text;

namespace MuzickiFestivali.Domain.Entities
{
    public class Nastupa
    {
        public int idOsoba { get; set; }
        public virtual Izvodjac izvodjac { get; set; }
        public int idTermin { get; set; }
        public int idFestival { get; set; }
        public int idNastup { get; set; }
        public virtual Termin termin { get; set; }
        public string uloga { get; set; }
        public bool potvrdenDolazak { get; set; }
        public string napomena { get; set; }
    }
}
