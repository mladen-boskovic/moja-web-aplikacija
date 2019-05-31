using System;
using System.Collections.Generic;

namespace WebServis.Models
{
    public partial class IspitniRok
    {
        public IspitniRok()
        {
            Ispit = new HashSet<Ispit>();
        }

        public short IdIspitniRok { get; set; }
        public short Godina { get; set; }
        public string IspitniRokNaziv { get; set; }
        public bool Popravni { get; set; }

        public ICollection<Ispit> Ispit { get; set; }
    }
}
