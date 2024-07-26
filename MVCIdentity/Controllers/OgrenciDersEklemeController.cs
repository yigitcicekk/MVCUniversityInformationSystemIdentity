using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCIdentity.Areas.Identity.Data;
using MVCIdentity.Data;
using MVCIdentity.Models;

namespace MVCIdentity.Controllers
{
    [Authorize(Roles = "Rektör,Ogrenci Isleri")]
    public class OgrenciDersEklemeController : Controller
    {
        private readonly MVCIdentityContext _db;
        private readonly UserManager<MVCIdentityUser> _userManager;

        public OgrenciDersEklemeController(MVCIdentityContext db,UserManager<MVCIdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        // Bu controller ise öğrenciye, öğrenci işleri veya başka bir yetkili tarafından ders eklenmesini sağlayacak.
        public async Task<IActionResult> Index()
        {

            // Burada öğrenciler ve dersler listelenmesi gereklidir.
            // Ona göre yetkili kişi, öğrenciyi ve eklemek istediği dersi seçmelidir. 
            ViewBag.TumDersler = await _db.Ders.ToListAsync();

            // Önce, ÖĞRENCİLERİN LİSTESİNİ GÖRSÜN 
            List<MVCIdentityUser> ogrenciler = new List<MVCIdentityUser>();
            foreach (var kullanici in await _userManager.Users.ToListAsync())
            {
                // her bir kullanıcının rollerini bul
                var kullaniciRolleri = await _userManager.GetRolesAsync(kullanici);
                // Kişinin rollerinde ogrenci rolu varsa öğrenciler listesine ata
                if (kullaniciRolleri.Any(r => r == "Ogrenci"))
                    ogrenciler.Add(kullanici);

            }
            ViewBag.Ogrenciler = ogrenciler;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(OgrenciDers ogrenciDers)
        {
            // formdan gelen öğrenci dersini ekle

            if (_db.OgrenciDersler.Any(od => od.MVCIdentityUserId == ogrenciDers.MVCIdentityUserId && od.DersId == ogrenciDers.DersId))
                TempData["Hata"] = "Eklemek istediğiniz ders, zaten kayıtlıdır.";
            else
            {

                // öğrenci dersini ekle
                _db.OgrenciDersler.AddAsync(ogrenciDers);
                await _db.SaveChangesAsync();

                TempData["Mesaj"] = "Ders Başarıyla Eklenmiştir.";
            }

            return RedirectToAction("Index");
        }

    }
}
