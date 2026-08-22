using System;
using System.Collections.Generic;
using System.Text;

namespace MuzickiFestivali.Domain.Entities
{
    public class Lajkuje
    {
        public int idOsoba { get; set; }
        public virtual Korisnik korisnik { get; set; }
        public int idNastup { get; set; }
        public int idFestival { get; set; }
        public virtual Nastup nastup { get; set; }
        public DateTime datumVremeLajka { get; set; }
    }
}
