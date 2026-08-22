using MuzickiFestivali.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MuzickiFestivali.Domain.Entities
{
    public class Nastup
    {
        public int idNastup { get; set; }
        public string naziv { get; set; }
        public string opis { get; set; }
        public Zanr zanr { get; set; }
        public int idFestival { get; set; }
        public virtual Festival festival { get; set; }
        public virtual ICollection<Termin> termini { get; set; }
        public virtual ICollection<Lajkuje> lajkovi { get; set; }
    }
}
