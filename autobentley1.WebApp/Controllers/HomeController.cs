using autobentley1.DataAccessLayer;
using autobentley1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace autobentley1.WebApp.Controllers
{
    public class HomeController : Controller
    {

        DatabaseContext context = new DatabaseContext();
        // GET: Home
        public ActionResult Index()
        {
            //Test test = new Test(); 
            return View(context.Adminler.ToList());
        }

        public ActionResult Hizmetlerimiz()
        {
            return View();
        }

        public ActionResult Iletisim(string situation)
        {
            if ( situation == null )
            {
                return View();
            }
            else
            {
                TempData["message"] = "MESAJINIZ EKLENDİ !";
                return View();
            }
            
            
        }

        [HttpPost]
        public ActionResult Iletisim(string name,string email,string phoneNumber,string message)
        {
            Form form = new Form();
            form.Name = name;
            form.Email = email;
            form.PhoneNumber = phoneNumber;
            form.Content = message;
            form.CreatedOn = DateTime.Now;
            context.Formlar.Add(form);
            context.SaveChanges();
           
            return RedirectToAction("Iletisim","Home",new { situation = "mesaj"});
        }

        public ActionResult FotoGaleri()
        {
            List<SoruCevap> fotolar = new List<SoruCevap>();
            fotolar = context.SoruCevaplar.Where(x => x.Type == "foto").ToList();
            return View(fotolar);
        }

        public ActionResult SoruCevap(string situation)
        {
            List<SoruCevap> sorular = new List<SoruCevap>();
            List<SoruCevap> cevaplar = new List<SoruCevap>();

            sorular = context.SoruCevaplar.Where(x=> x.Type == "soru").OrderByDescending(x => x.Id).Take(20).ToList();
            cevaplar = context.SoruCevaplar.Where(x => x.Type == "cevap").OrderByDescending(x => x.Id).Take(20).ToList();
            TempData["cevaplar"] = cevaplar;

            if ( situation == null )
            {
                return View(sorular);
            }
            else
            {
                TempData["message"] = "MESAJINIZ EKLENDİ !";
                return View(sorular);
            }
            
        }

        [HttpPost]
        public ActionResult SoruCevap(string name,string message,string email)
        {
            SoruCevap sorucvp = new SoruCevap();
            sorucvp.Name = name;
            sorucvp.Email = email;
            sorucvp.CreatedOn = DateTime.Now;
            sorucvp.Message = message;
            sorucvp.Type = "soru";
            context.SoruCevaplar.Add(sorucvp);
            context.SaveChanges();
            return RedirectToAction("SoruCevap","Home",new { situation = "mesaj"});
        }

        
    }

       
}