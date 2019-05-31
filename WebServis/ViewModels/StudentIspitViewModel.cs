using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebServis.ViewModels
{
    public class StudentIspitViewModel
    {
        public string PredmetNaziv { get; set; }
        public byte? Ocena { get; set; }
        public DateTime? IspitDatum { get; set; }
    }
}
