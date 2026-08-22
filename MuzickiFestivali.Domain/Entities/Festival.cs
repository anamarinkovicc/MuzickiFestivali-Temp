using System;
using System.Collections.Generic;
using System.Text;

namespace MuzickiFestivali.Domain.Entities
{
    public class Festival
    {
        public int idFestival { get; set; }
        public string naziv { get; set; }
        public string opis { get; set; }
        public DateTime datumPocetka { get; set; }
        public DateTime datumZavrsetka { get; set; }
        public int kapacitet { get; set; }
        public int idOsoba { get; set; }
        public virtual Zaposleni zaposleni { get; set; }
        public virtual ICollection<Nastup> nastupi { get; set; }
    }
}
