using MuzickiFestivali.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MuzickiFestivali.Domain.Entities
{
    public class Izvodjac : Osoba
    {
        public string umetnickoIme { get; set; }
        public string biografija { get; set; }
        public Zanr zanr { get; set; }
        public virtual ICollection<Nastupa> nastupa { get; set; }
    }
}
