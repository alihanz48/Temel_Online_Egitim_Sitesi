using System.Threading.Tasks;
using EntityFrameworkCoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCoreApp.Controllers
{
    public class KursKayitController : Controller
    {

        public readonly DataContext _context;
        public KursKayitController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var kurskayitlari=await _context.KursKayitlari.Include(x=>x.Ogrenci).Include(x=>x.Kurs).ToListAsync();
            return View(kurskayitlari);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Ogrenciler = new SelectList(await _context.Ogrenciler.ToListAsync(), "OgrenciId", "AdSoyad");
            ViewBag.Kurslar = new SelectList(await _context.Kurslar.ToListAsync(), "KursId", "KursBaslik");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KursKayit model)
        {
            if (model.KursId == -1 || model.OgrenciId == -1)
            {
                ViewBag.Ogrenciler = new SelectList(await _context.Ogrenciler.ToListAsync(), "OgrenciId", "AdSoyad");
                ViewBag.Kurslar = new SelectList(await _context.Kurslar.ToListAsync(), "KursId", "KursBaslik");
                ViewData["err"]="Lütfen öğrenci ve kurs seçiniz";
                return View(model);
            }
            else
            {
                model.KayitTarihi = DateTime.Now;
                _context.KursKayitlari.Add(model);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

        }
    }
}