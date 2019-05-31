using System;
using System.Collections.Generic;

namespace WebServis.Models
{
    public partial class Predmet
    {
        public Predmet()
        {
            Ispit = new HashSet<Ispit>();
        }

        public short IdPredmet { get; set; }
        public string PredmetNaziv { get; set; }
        public byte Espb { get; set; }

        public ICollection<Ispit> Ispit { get; set; }
    }
}
