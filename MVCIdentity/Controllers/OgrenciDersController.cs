using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCIdentity.Areas.Identity.Data;
using MVCIdentity.Data;
using MVCIdentity.Models;

namespace MVCIdentity.Controllers
{
    // Öğrenci rolüne ait olan kullanıcılar bu controller'a erişebilir.
    [Authorize(Roles = "Ogrenci")]

    // Burası öğrencilerin ders seçimlerinin/çıkartılmalarının yapılması için
    public class OgrenciDersController : Controller
    {
        private readonly MVCIdentityContext _db;
        private readonly UserManager<MVCIdentityUser> _userManager;

        public OgrenciDersController(MVCIdentityContext db,UserManager<MVCIdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Bir combobox'ta tüm dersler olmalıdır. Öğrenci ise bu combobox'tan ders seçebilmelidir. Ayrıca seçtiği dersi silebilmelidir. Var olan dersi de kendisine bir daha EKLEYEMEMELİDİR.
            // Madem ki öğrenci kendisine ders ekleyip çıkaracak o zaman sistemi o anda kullanan ANLIK kullanıcıyı bulmamız gereklidir.

            // Ayrıca burada öğrencinin seçtiği dersleride göreceğiz. O yüzden öğrencinin seçtiği dersleride getirmemiz gereklidir.

            // Anlık kullanıcı id'si nasıl alınır
            // Cevap: 
            var kullaniciId = _userManager.GetUserId(HttpContext.User);  
            
            ViewBag.TumDersler = await _db.Ders.ToListAsync();
            ViewBag.SeciliDersler = await _db.OgrenciDersler.Include(od => od.Ders).Where(od => od.MVCIdentityUserId == kullaniciId).ToListAsync();

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(OgrenciDers ogrenciDers)
        {

            var kullaniciId = _userManager.GetUserId(HttpContext.User);

            if(_db.OgrenciDersler.Any(od => od.MVCIdentityUserId == kullaniciId && od.DersId == ogrenciDers.DersId))
                TempData["Hata"] = "Eklemek istediğiniz ders, zaten kayıtlıdır.";
            else
            {
                // ögrenci dersinin kullanıcı id'sini ata
                // (öğrenci dersinin ders id'sini zaten formdan atanmış olarak gelecek.
                ogrenciDers.MVCIdentityUserId = kullaniciId;

                // şimdi de bu öğrenci dersini ekle
                _db.OgrenciDersler.AddAsync(ogrenciDers);
                await _db.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Sil(int id)
        {
            // yakalanan ogrenciders id ile ogrencidersini getirelim ve emin misiniz ekranına model olarak yönlendirelim.

            return View(await _db.OgrenciDersler.Include(od => od.Ders).FirstOrDefaultAsync(od => od.Id == id));
        }
        [HttpPost]
        public async Task<IActionResult> Sil(OgrenciDers ogrenciDers)
        {
            // formdan gelen ogrenci dersini SİLECEK
            _db.OgrenciDersler.Remove(ogrenciDers);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
