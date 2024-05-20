using MVC_StokTakip.Models.Entity_Framework;
using PagedList;
using System.Linq;
using System.Web.Mvc;

namespace MVC_StokTakip.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori

        MVC_DBStokEntities1 db = new MVC_DBStokEntities1();

        public ActionResult Index(int sayfa = 1)
        {
            //var degerler = db.TBL_KATEGORILER.ToList();
            var degerler = db.TBL_KATEGORILER.ToList().ToPagedList(sayfa , 10);
            return View(degerler);
        }

        [HttpGet] // butona tıklama olmadan sayfa yüklenirken sadece view'ı döndürecektir.
        public ActionResult YeniKategori()
        {
            return View();
        }

        [HttpPost] // sayfada butona tıklama olursa değişiklikler yapıldıktan sonra view'ı döndürecektir.
        public ActionResult YeniKategori(TBL_KATEGORILER p1)
        {
            if(!ModelState.IsValid)
            {
                return View("YeniKategori");
            }
            db.TBL_KATEGORILER.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Sil(int id ) // sil butonuna sil komutunu girdik.
        {
            var kategori = db.TBL_KATEGORILER.Find(id);
            db.TBL_KATEGORILER.Remove(kategori);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KategoriGetir(int id )
        {
            var ktgr = db.TBL_KATEGORILER.Find(id);
            return View("KategoriGetir", ktgr);
        }

        public ActionResult Guncelle(TBL_KATEGORILER p1)
        {
            var ktg = db.TBL_KATEGORILER.Find(p1.KATEGORIID);
            ktg.KATEGORIADI = p1.KATEGORIADI;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}