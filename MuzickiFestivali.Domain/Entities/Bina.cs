using System;
using System.Collections.Generic;
using System.Text;

namespace MuzickiFestivali.Domain.Entities
{
    public class Bina
    {
        public int idBina { get; set; }
        public string naziv { get; set; }
        public int kapacitet { get; set; }
        public float xKoordinata { get; set; }
        public float yKoordinata { get; set; }
        public virtual ICollection<Termin> termini { get; set; }
    }
}
