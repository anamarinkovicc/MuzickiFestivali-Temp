using MuzickiFestivali.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MuzickiFestivali.Domain.Entities
{
    public class Korisnik : Osoba
    {
        public Zanr omiljeniZanr { get; set; }
        public virtual ICollection<Lajkuje> lajkuje { get; set; }
    }
}
