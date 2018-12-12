using autobentley1.DataAccessLayer;
using autobentley1.Entities;
using autobentley1.WebApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace autobentley1.WebApp.Controllers
{
    public class AdminController : Controller
    {

        DatabaseContext context = new DatabaseContext();
        
        // GET: Admin
        public ActionResult Index( string message )
        {
            if( message == null)
            {
                return View();
            }
            else
            {
                TempData["message"] = "KULLANICI ADI YADA ŞİFRE HATALI";
                return View();
            }
            
        }

        [HttpPost]
        public ActionResult Index(string userName,string password)
        {
            
            Admin admin = new Admin();
            admin = context.Adminler.Where(x => x.Username == userName && x.Password == password).FirstOrDefault();
            if ( admin == null )
            {
                return RedirectToAction("Index", "Admin",new { message = "HATA"});
            }
            else
            {
                CurrentSession.Set<Admin>("login", admin);
                return RedirectToAction("AdminPanel", "Admin");
                
            }        
        }

        public ActionResult AdminPanel()
        {
            if( CurrentSession.currentUser != null )
            {
                Admin admin = new Admin();
                List<Form> formlar = new List<Form>();
                formlar = context.Formlar.OrderByDescending(x => x.Id).ToList();
                TempData["Mails"] = formlar;
                if (CurrentSession.currentUser == null)
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    return View(CurrentSession.currentUser);
                }
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }

            
        }

        public ActionResult SoruCevaplarAdmin()
        {
            if( CurrentSession.currentUser != null )
            {
                List<SoruCevap> sorular = new List<SoruCevap>();
                List<SoruCevap> cevaplar = new List<SoruCevap>();

                sorular = context.SoruCevaplar.Where(x => x.Type == "soru").OrderByDescending(x => x.Id).Take(20).ToList();
                cevaplar = context.SoruCevaplar.Where(x => x.Type == "cevap").OrderByDescending(x => x.Id).Take(20).ToList();
                TempData["cevaplar"] = cevaplar;

                return View(sorular);
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }

            
        }

        [HttpPost]
        public ActionResult SoruCevaplarAdmin(string message,string baginti)
        {

            

            SoruCevap cevap = new SoruCevap();
            cevap.Baginti = baginti;
            cevap.Message = message;
            cevap.CreatedOn = DateTime.Now;
            cevap.Type = "cevap";
            context.SoruCevaplar.Add(cevap);
            context.SaveChanges();

            return RedirectToAction("SoruCevaplarAdmin","Admin");
        }

        public ActionResult MesajSil(int mesajId)
        {
            if(CurrentSession.currentUser != null)
            {
                List<SoruCevap> sorular = new List<SoruCevap>();

                sorular = context.SoruCevaplar.Where(x => x.Id == mesajId || x.Baginti == mesajId.ToString()).ToList();
                foreach ( SoruCevap soru in sorular )
                {
                    context.SoruCevaplar.Remove(soru);
                }
                context.SaveChanges();

                return RedirectToAction("SoruCevaplarAdmin", "Admin");
            }
            else
            {
                return HttpNotFound();
            }
        }

        public ActionResult FotoDuzenleAdmin(string message)
        {
            if ( CurrentSession.currentUser != null )
            {
                List<SoruCevap> fotolar = new List<SoruCevap>();
                fotolar = context.SoruCevaplar.Where( x=> x.Type == "foto" ).ToList();
                if (message != null )
                {
                    TempData["message"] = message;
                }
                return View(fotolar);
            }
            else
            {
                return RedirectToAction("Index", "Admin");
            }
            
        }

        public ActionResult FotoDuzenleEklePost(HttpPostedFileBase uploadFile)
        {
            if (CurrentSession.currentUser != null)
            {
                if (uploadFile == null)
                {
                    return RedirectToAction("FotoDuzenleAdmin", "Admin", new { ilanId = CurrentSession.currentIlanId });
                }
                
                SoruCevap foto = new SoruCevap();

                string filepath = Path.Combine(Server.MapPath("~/images/"), Path.GetFileName(uploadFile.FileName));
                uploadFile.SaveAs(filepath);             
                foto.Message = uploadFile.FileName;
                foto.CreatedOn = DateTime.Now;
                foto.Type = "foto";
                context.SoruCevaplar.Add(foto);
                context.SaveChanges();
                return RedirectToAction("FotoDuzenleAdmin", "Admin",new { message = "FOTOĞRAF EKLEME BAŞARILI !"});
            }
            else
            {
                return HttpNotFound();
            }

        }

        public ActionResult FotoDuzenleSilPost(int fotoId)
        {
            SoruCevap foto = new SoruCevap();
            foto = context.SoruCevaplar.Where(x => x.Id == fotoId).FirstOrDefault();
            context.SoruCevaplar.Remove(foto);
            context.SaveChanges();
            return RedirectToAction("FotoDuzenleAdmin", "Admin");
        }





    }
}