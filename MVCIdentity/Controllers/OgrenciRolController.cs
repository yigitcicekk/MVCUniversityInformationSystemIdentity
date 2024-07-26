using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCIdentity.Areas.Identity.Data;
using MVCIdentity.Data;

namespace MVCIdentity.Controllers
{
    [Authorize(Roles = "Rektör,Ogrenci Isleri")]
    public class OgrenciRolController : Controller
    {
        private readonly UserManager<MVCIdentityUser> _userManager;
        private readonly MVCIdentityContext _db;

        // Kullanıcılarla ilgili işlemler yapacağımız için Identity'den gelen UserManager'ı kullanacağız
        public OgrenciRolController(UserManager<MVCIdentityUser> userManager,MVCIdentityContext db)
        {
            _userManager = userManager;
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            // Bütün kullanıcıları getirip view'ımıza aktaralım.
            // Aktarabilmek için _usermanager'a ihtiyacımız var

            // Bu _usermanager bize hem kullanıcıları getirmeyi hem de kullanıcılara rol atama ve silmeyi sağlayacak
            return View(await _userManager.Users.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> RolEkle(string id)
        {
            // Burası sadece eklemek istediğinize emin misiniz ekranını açacak
            return View(await _userManager.FindByIdAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> RolEkle(MVCIdentityUser user)
        {
            // Emin misiniz ekranında EVET'e basıldığı zaman çalışan action olacak. Dolayısıyla ekleme işlemini gerçekleştirecek.
            var guncellenecekKullanici = await _userManager.FindByIdAsync(user.Id);

            await _userManager.AddToRoleAsync(guncellenecekKullanici, "Ogrenci");

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> RolKaldir(string id)
        {
            // Burası sadece eklemek istediğinize emin misiniz ekranını açacak
            return View(await _userManager.FindByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> RolKaldir(MVCIdentityUser user)
        {
            // Emin misiniz ekranında EVET'e basıldığı zaman çalışan action olacak. Dolayısıyla ekleme işlemini gerçekleştirecek.
            var silinecekKullanici = await _userManager.FindByIdAsync(user.Id);

            if(_db.OgrenciDersler.Any(od => od.MVCIdentityUserId == silinecekKullanici.Id))            
                TempData["Hata"] = "Öğrenci rolünü kaldırmak istediğiniz kişiye ait dersler bulunmaktadır.Lütfen önce o dersleri silip tekrar deneyiniz.";
            else
                await _userManager.RemoveFromRoleAsync(silinecekKullanici, "Ogrenci");

            return RedirectToAction("Index");
        }
    }
}
