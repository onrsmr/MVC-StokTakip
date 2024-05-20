using MVC_StokTakip.Models.Entity_Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_StokTakip.Controllers
{
    public class SatislarController : Controller
    {
        // GET: Satislar
        MVC_DBStokEntities1 db = new MVC_DBStokEntities1();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet] // butona tıklama olmadan sayfa yüklenirken sadece view'ı döndürecektir.
        public ActionResult YeniSatis()
        {
            return View();
        }

        [HttpPost] // sayfada butona tıklama olursa değişiklikler yapıldıktan sonra view'ı döndürecektir.
        public ActionResult YeniSatis(TBL_SATISLAR p1) 
        {
            db.TBL_SATISLAR.Add(p1);
            db.SaveChanges();
            return View("Index");
        }
    }
}