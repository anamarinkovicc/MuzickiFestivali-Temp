using System;
using System.Collections.Generic;
using System.Text;

namespace MuzickiFestivali.Domain.Entities
{
    public abstract class Osoba
    {
        public int idOsoba { get; set; }
        public string ime { get; set; }
        public string prezime { get; set; }
        public string email { get; set; }
        public string lozinka { get; set; }
    }
}
