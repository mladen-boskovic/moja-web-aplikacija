using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WpfKlijent.Core;
using WpfKlijent.DataLayer;

namespace WpfKlijent
{
    public class MainWindowViewModel : PretplacenaKlasa<MainWindowViewModel>
    {
        private ObservableCollection<Student> students;
        private ObservableCollection<StudentIspit> studentsIspits;
        private string poljeIme;
        private string poljePrezime;
        private Student izabraniStudent;
        HttpClient client = new HttpClient();

        public MainWindowViewModel()
        {
            this.client.BaseAddress = new Uri("http://localhost:51489/");
            dohvatiStudente();
        }

        public ObservableCollection<Student> Students
        {
            get
            {
                return this.students;
            }
            set
            {
                this.pretplatiSeNaPromenu(ref students, value);
            }
        }

        public ObservableCollection<StudentIspit> StudentsIspits
        {
            get
            {
                return this.studentsIspits;
            }
            set
            {
                this.pretplatiSeNaPromenu(ref studentsIspits, value);
            }
        }

        public string PoljeIme
        {
            get
            {
                return this.poljeIme;
            }
            set
            {
                pretplatiSeNaPromenu(ref poljeIme, value);
            }
        }

        public string PoljePrezime
        {
            get
            {
                return this.poljePrezime;
            }
            set
            {
                pretplatiSeNaPromenu(ref poljePrezime, value);
            }
        }

        public Student IzabraniStudent {
            get
            {
                return this.izabraniStudent;
            }
            set
            {
                this.izabraniStudent = value;
                if(izabraniStudent != null)
                {
                    this.PoljeIme = izabraniStudent.Ime;
                    this.PoljePrezime = izabraniStudent.Prezime;

                    HttpResponseMessage message = this.client.GetAsync("api/ispiti_studenta/" + izabraniStudent.IdStudent).Result;
                    var podaci = message.Content.ReadAsAsync<IEnumerable<StudentIspit>>().Result;
                    this.StudentsIspits = new ObservableCollection<StudentIspit>(podaci);
                }
            }
        }

        public void dohvatiStudente()
        {
            HttpResponseMessage message = this.client.GetAsync("api/svi_studenti").Result;
            var podaci = message.Content.ReadAsAsync<IEnumerable<Student>>().Result;
            this.Students = new ObservableCollection<Student>(podaci);
        }

        public void InsertUpdate(object sender, EventArgs e)
        {
            if(izabraniStudent == null)
            {
                HttpResponseMessage message = client.PostAsJsonAsync("api/dodaj_studenta",
                   new Student
                   {
                       Ime = poljeIme,
                       Prezime = PoljePrezime
                   }).Result;
                dohvatiStudente();
            }
            else
            {
                HttpResponseMessage message = client.PutAsJsonAsync("api/izmeni_studenta/" + izabraniStudent.IdStudent,
                    new Student
                    {
                        Ime = poljeIme,
                        Prezime = PoljePrezime
                    }).Result;
                dohvatiStudente();
            }
        }

        public void DeleteStudent(object sender, EventArgs e)
        {
            if (izabraniStudent == null)
                return;
            HttpResponseMessage message = client.DeleteAsync("api/obrisi_studenta/" + izabraniStudent.IdStudent).Result;
            dohvatiStudente();
        }
    }
}
