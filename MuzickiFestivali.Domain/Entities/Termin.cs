using MuzickiFestivali.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MuzickiFestivali.Domain.Entities
{
    public class Termin
    {
        public int idTermin { get; set; }
        public TipTermina tip { get; set; }
        public DateTime vremePocetka { get; set; }
        public DateTime vremeZavrsetka { get; set; }
        public string? napomena { get; set; }
        public int idNastup { get; set; }
        public int idFestival { get; set; }
        public virtual Nastup nastup { get; set; }
        public int idBina { get; set; }
        public virtual Bina bina { get; set; }
        public virtual ICollection<Nastupa> izvodjaci { get; set; }
    }
}
