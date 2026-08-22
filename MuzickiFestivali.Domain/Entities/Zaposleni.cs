using System;
using System.Collections.Generic;
using System.Text;

namespace MuzickiFestivali.Domain.Entities
{
    public class Zaposleni : Osoba
    {
        public string pozicija { get; set; }
        public virtual ICollection<Festival> festivali { get; set; }
    }
}
