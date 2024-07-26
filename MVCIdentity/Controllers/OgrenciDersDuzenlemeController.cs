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
    public class OgrenciDersDuzenlemeController : Controller
    {
        private readonly UserManager<MVCIdentityUser> _userManager;
        private readonly MVCIdentityContext _db;
        private readonly RoleManager<IdentityRole> _roleManager;

        public OgrenciDersDuzenlemeController(UserManager<MVCIdentityUser> userManager,MVCIdentityContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            // Önce, ÖĞRENCİLERİN LİSTESİNİ GÖRSÜN 
            List<MVCIdentityUser> ogrenciler = new List<MVCIdentityUser>();
            foreach (var kullanici in await _userManager.Users.ToListAsync())
            {
                // her bir kullanıcının rollerini bul
                var kullaniciRolleri = await _userManager.GetRolesAsync(kullanici);
                // Kişinin rollerinde ogrenci rolu varsa öğrenciler listesine ata
                if (kullaniciRolleri .Any(r => r == "Ogrenci"))
                    ogrenciler.Add(kullanici);
   
            }

            return View(ogrenciler);
        }

        public async Task<IActionResult> DersleriGor(string id)
        {

            MVCIdentityUser goruntulenecekOgrenci;

            //öğrencinin asp-route-id ile gelen id'sini kullanarak seçtiği derslerini(OgrenciDers.cs) getirip view'ına model olarak gönderelim ki view'da görüntülensin.

            // öğrencinin adı ile soyadını açılan ekranda yazdırabilmek için o kişiyi getirip adını ve soyadını view'a aktaralım.
            
            if(id == null)
            {
                id = TempData["OgrenciId"].ToString();
            }
            goruntulenecekOgrenci = await _userManager.FindByIdAsync(id);

            ViewData["Ad"] = goruntulenecekOgrenci.FirstName;
            ViewData["Soyad"] = goruntulenecekOgrenci.LastName;

            return View(await _db.OgrenciDersler.Include(od => od.Ders).Where(od => od.MVCIdentityUserId == id).ToListAsync());
        }

        public async Task<IActionResult> Sil(int id)
        {
            //asp-route-id'den gelen id'yi kullanarak ogrencidersini getir ve emin misiniz ekranına (kendi view'ı) aktar.
            return View(await _db.OgrenciDersler.Include(od => od.Ders).FirstOrDefaultAsync(od => od.Id == id));
        }
        [HttpPost]
        public async Task<IActionResult> Sil(OgrenciDers ogrenciDers)
        {
            _db.OgrenciDersler.Remove(ogrenciDers);
            await _db.SaveChangesAsync();
            TempData["OgrenciId"] = ogrenciDers.MVCIdentityUserId;
            return RedirectToAction("DersleriGor");
        }
    }
}
