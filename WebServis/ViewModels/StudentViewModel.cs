using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebServis.Models;

namespace WebServis.ViewModels
{
    public class StudentViewModel
    {
        public int IdStudent { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
    }
}
