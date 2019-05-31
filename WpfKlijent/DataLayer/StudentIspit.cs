using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfKlijent.DataLayer
{
    public class StudentIspit
    {
        public string PredmetNaziv { get; set; }
        public byte? Ocena { get; set; }
        public DateTime? IspitDatum { get; set; }
    }
}
