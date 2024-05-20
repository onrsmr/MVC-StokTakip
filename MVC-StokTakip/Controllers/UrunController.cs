using MVC_StokTakip.Models.Entity_Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_StokTakip.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        
        MVC_DBStokEntities1 db = new MVC_DBStokEntities1();

        public ActionResult Index()
        {
            var degerler = db.TBL_URUNLER.ToList();
            return View(degerler);
        }

        [HttpGet] // butona tıklama olmadan sayfa yüklenirken sadece view'ı döndürecektir.
        public ActionResult YeniUrun()
        {
            List<SelectListItem> degerler = (from i in db.TBL_KATEGORILER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIADI.ToString(),
                                                 Value = i.KATEGORIID.ToString(),
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View();
        }

        [HttpPost] // sayfada butona tıklama olursa değişiklikler yapıldıktan sonra view'ı döndürecektir.
        public ActionResult YeniUrun(TBL_URUNLER p1) // p1 bir parametredir.
        {
            var ktg= db.TBL_KATEGORILER.Where(m => m.KATEGORIID == p1.TBL_KATEGORILER.KATEGORIID).FirstOrDefault();
            p1.TBL_KATEGORILER = ktg;
            db.TBL_URUNLER.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Sil(int id)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniUrun");
            }
            var urunler = db.TBL_URUNLER.Find(id);
            db.TBL_URUNLER.Remove(urunler);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrunGetir(int id)
        {
            var urn = db.TBL_URUNLER.Find(id);

            List<SelectListItem> degerler = (from i in db.TBL_KATEGORILER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIADI.ToString(),
                                                 Value = i.KATEGORIID.ToString(),
                                             }).ToList();
            ViewBag.dgr = degerler;

            return View("UrunGetir", urn);
        }

        public ActionResult Guncelle(TBL_URUNLER p1)
        {
            var urn = db.TBL_URUNLER.Find(p1.URUNID);
            urn.URUNAD = p1.URUNAD;
            urn.MARKA = p1.MARKA;
            
            var ktg = db.TBL_KATEGORILER.Where(m => m.KATEGORIID == p1.TBL_KATEGORILER.KATEGORIID).FirstOrDefault();
            urn.URUNKATEGORI = ktg.KATEGORIID;
            
            urn.FIYAT = p1.FIYAT;
            urn.STOK = p1.STOK;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}