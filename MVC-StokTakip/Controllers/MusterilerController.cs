using MVC_StokTakip.Models.Entity_Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace MVC_StokTakip.Controllers
{
    public class MusterilerController : Controller
    {
        // GET: Musteriler

        MVC_DBStokEntities1 db = new MVC_DBStokEntities1();

        public ActionResult Index(string p)
        {
            var degerler = from d in db.TBL_MUSTERILER select d;
            if (!string.IsNullOrEmpty(p))
            {
                degerler = degerler.Where(m => m.MUSTERIAD.Contains(p));
            }
            return View(degerler.ToList());
        }

        [HttpGet] // butona tıklama olmadan sayfa yüklenirken sadece view'ı döndürecektir.
        public ActionResult YeniMusteri()
        {
            return View();
        }

        [HttpPost] // sayfada butona tıklama olursa değişiklikler yapıldıktan sonra view'ı döndürecektir.
        public ActionResult YeniMusteri(TBL_MUSTERILER p1) // p1 bir parametredir. Ekle sil veya güncelle parametresi olarak da tanımlanabilir.
        {
            if (!ModelState.IsValid)
            {
                return View("YeniMusteri");
            }
            db.TBL_MUSTERILER.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Sil(int id)
        {
            var musteri = db.TBL_MUSTERILER.Find(id);
            db.TBL_MUSTERILER.Remove(musteri);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult MusteriGetir(int id )
        {
            var mstr = db.TBL_MUSTERILER.Find(id);
            return View("MusteriGetir", mstr);
        }
        
        public ActionResult Guncelle(TBL_MUSTERILER p1)
        {
            var mstr = db.TBL_MUSTERILER.Find(p1.MUSTERIID);
            mstr.MUSTERIAD = p1.MUSTERIAD;
            mstr.MUSTERISOYAD =p1.MUSTERISOYAD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}