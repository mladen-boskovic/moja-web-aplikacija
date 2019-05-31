using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebServis.Models;
using WebServis.ViewModels;

namespace WebServis.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly CUSERSMLADEDOCUMENTSVISUALSTUDIO2017PROJECTSMOJAWEBAPLIKACIJASTUDIJEMDFContext context;
        public StudentsController(CUSERSMLADEDOCUMENTSVISUALSTUDIO2017PROJECTSMOJAWEBAPLIKACIJASTUDIJEMDFContext context)
        {
            this.context = context;
        }

        // GET: api/svi_studenti
        [Route("api/svi_studenti")]
        [HttpGet]
        public IQueryable<StudentViewModel> Get() //DOHVATANJE SVIH STUDENATA
        {
            var studenti = this.context.Student.Select(x => new StudentViewModel
            {
                IdStudent = x.IdStudent,
                Ime = x.Ime,
                Prezime = x.Prezime
            });
            return studenti;
        }

        // GET: api/students/5
        [Route("api/students/{id}")]
        [HttpGet("{id}")] //, Name = "Get"
        public StudentViewModel Get(int id) //DOHVATANJE JEDNOG STUDENTA PO ID-U
        {
            var student = this.context.Student.Find(id);
            if(student != null)
            {
                return new StudentViewModel
                {
                    IdStudent = student.IdStudent,
                    Ime = student.Ime,
                    Prezime = student.Prezime
                };
            }
            return null;
        }

        // GET: api/students/paja/patak
        [Route("api/students/{ime}/{prezime}")]
        [HttpGet("{ime}/{prezime}")] //, Name = "Get"
        public StudentViewModel Get(string ime, string prezime) //DOHVATANJE JEDNOG STUDENTA PO IMENU I PREZIMENU
        {
            #region drugi nacin
            //var student = (from stud in this.context.Student
            //               where (stud.Ime == ime && stud.Prezime == prezime)
            //               select new StudentViewModel
            //               {
            //                   IdStudent = stud.IdStudent,
            //                   Ime = stud.Ime,
            //                   Prezime = stud.Prezime
            //               }).FirstOrDefault(); 
            #endregion

            var student = this.context.Student.Where(x => (x.Ime == ime && x.Prezime == prezime)).Select(x => new StudentViewModel
            {
                IdStudent = x.IdStudent,
                Ime = x.Ime,
                Prezime = x.Prezime
            }).FirstOrDefault();

            if (student != null)
                return student;
            return null;
        }
        
        // GET: api/students/svi/paja
        [Route("api/students/svi/{ime}")]
        [HttpGet("{ime}")] //, Name = "Get"
        public IQueryable<StudentViewModel> Get(string ime) //DOHVATANJE VISE STUDENATA PO IMENU
        {
            var student = from stud in this.context.Student
                          where stud.Ime == ime
                          select new StudentViewModel
                          {
                              IdStudent = stud.IdStudent,
                              Ime = stud.Ime,
                              Prezime = stud.Prezime
                          };
            if (student != null)
                return student;
            return null;
        }

        // GET: api/ispiti_studenta/5
        [Route("api/ispiti_studenta/{id}")]
        [HttpGet("{id}")] //, Name = "Get"
        public IQueryable<StudentIspitViewModel> GetIspiti(int id) //DOHVATANJE POLOZENIH ISPITA PROSLEDJENOG STUDENTA
        {
            var ispiti = (from student in this.context.Student
                          join studentIspit in this.context.StudentIspit
                          on student.IdStudent equals studentIspit.IdStudent
                          join ispit in this.context.Ispit
                          on studentIspit.IdIspit equals ispit.IdIspit
                          join predmet in this.context.Predmet
                          on ispit.IdPredmet equals predmet.IdPredmet
                          where (student.IdStudent == id && studentIspit.Ocena > 5)
                          select new StudentIspitViewModel
                          {
                              Ocena = studentIspit.Ocena,
                              IspitDatum = ispit.IspitDatum,
                              PredmetNaziv = predmet.PredmetNaziv
                          });
            if (ispiti != null)
                return ispiti;
            return null;
        }

        // POST: api/dodaj_studenta
        [Route("api/dodaj_studenta")]
        [HttpPost]
        public void Post(StudentViewModel s) //INSERT STUDENTA (radi i bez [FromBody])
        {
            this.context.Student.Add(new Student
            {
                Ime = s.Ime,
                Prezime = s.Prezime
            });
            this.context.SaveChanges();
        }

        // PUT: api/izmeni_studenta/5
        [Route("api/izmeni_studenta/{id}")]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] StudentViewModel s) //UPDATE STUDENTA
        {
            var student = new Student
            {
                IdStudent = id, //ID STUDENTA KOJI SE MENJA
                Ime = s.Ime,
                Prezime = s.Prezime
            };
            this.context.Student.Update(student);
            this.context.SaveChanges();
        }

        // DELETE: api/obrisi_studenta/5
        [Route("api/obrisi_studenta/{id}")]
        [HttpDelete("{id}")]
        public void Delete(int id) //BRISANJE STUDENTA
        {
            IQueryable<StudentIspit> ispiti = from student_ispit in this.context.StudentIspit
                                              where student_ispit.IdStudent == id
                                              select student_ispit;
            this.context.StudentIspit.RemoveRange(ispiti);
            var student = this.context.Student.Find(id);
            if(student != null)
            {
                this.context.Student.Remove(student);
                this.context.SaveChanges();
            }
        }
    }
}
