using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using TechCareerMVCFinal.Data;
using TechCareerMVCFinal.Models;

namespace TechCareerMVCFinal.Controllers
{
    //[Authorize(Roles = UserRoles.Role_Admin)]
    //[Authorize(Roles = UserRoles.Role_Kullanici)]
    [Authorize(Roles = "Admin, Kullanici")]
    public class SiparisVermeController : Controller
    {
        private readonly IKiyafetRepository _kiyafetRepository;
        private readonly ISiparisVermeRepository _siparisVermeRepository;
        public readonly IWebHostEnvironment _webHostEnvironment;

        public SiparisVermeController(ISiparisVermeRepository context, IKiyafetRepository kiyafetRepository, IWebHostEnvironment webHostEnvironment)
        {
            _siparisVermeRepository = context;
            _kiyafetRepository = kiyafetRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return RedirectToAction("Index", "Home");
            }

            string userId = userIdClaim.Value;
            ViewBag.SepetAdet = _siparisVermeRepository.GetAll().Count(x => x.kullaniciId == userId);

            List<SiparisVerme> siparisVermeList = _siparisVermeRepository.GetAll(includeProps: "Kiyafet").ToList();
            return View(siparisVermeList);
        }

        public IActionResult SepeteEkle(int kiyafetId)
        {
            // Kullanıcı kimliğini al
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim != null)
            {
                string userId = userIdClaim.Value;

                // Sepete ekleme işlemini gerçekleştir
                // Örnek bir işlem:
                // Sipariş verme nesnesi oluştur ve sepete eklemeyi gerçekleştir
                var siparisVerme = new SiparisVerme
                {
                    kullaniciId = userId,
                    KiyafetId = kiyafetId
                };

                // Sepete ekleme işlemini gerçekleştir
                _siparisVermeRepository.Ekle(siparisVerme);
                _siparisVermeRepository.Kaydet();

                // Başarılı bir şekilde sepete eklendi mesajı gönder
                TempData["basarili"] = "Ürün başarıyla sepete eklendi.";

                // İşlem tamamlandıktan sonra kullanıcıyı uygun bir sayfaya yönlendir
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Kullanıcı kimliği bulunamadı veya yok.
                // Burada uygun bir hata işleme stratejisi uygulayabilirsiniz.
                TempData["hata"] = "Kullanıcı kimliği bulunamadı veya geçersiz.";
                return RedirectToAction("Index", "Home");
            }
        }


        public IActionResult Sepet()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return RedirectToAction("Index", "Home");
            }

            string userId = userIdClaim.Value;
            var sepetItems = _siparisVermeRepository.GetAll(includeProps: "Kiyafet").Where(x => x.kullaniciId == userId).ToList();
            return View(sepetItems);
        }

        //public IActionResult SiparisVer(SiparisVerme siparisVerme)
        //{
        //    var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        //    if (userIdClaim != null)
        //    {
        //        string userId = userIdClaim.Value;

        //        var sepetItems = _siparisVermeRepository.GetAll(includeProps: "Kiyafet").Where(x => x.kullaniciId == userId).ToList();

        //        // Sipariş nesnesini oluştururken kullanıcı kimliğini atayın
        //        var yeniSiparis = new SiparisVerme
        //        {
        //            kullaniciId = userId,
        //            KiyafetId = siparisVerme.Kiyafet.Id,
        //        };

        //        _siparisVermeRepository.Ekle(yeniSiparis);

        //        // Siparişleri kaydedin
        //        _siparisVermeRepository.Kaydet();

        //        // Sepet verilerini temizle
        //        foreach (var item in sepetItems)
        //        {
        //            _siparisVermeRepository.Sil(item);
        //        }
        //        _siparisVermeRepository.Kaydet();

        //        TempData["basarili"] = "Siparişler başarılı bir şekilde oluşturuldu.";
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        TempData["hata"] = "Kullanıcı kimliği bulunamadı veya geçersiz.";
        //        return RedirectToAction("Index", "Home");
        //    }
        //}


        public IActionResult SiparisVermeEkleGuncelle(int? id)
        {
            IEnumerable<SelectListItem> kiyafetList = _kiyafetRepository.GetAll()
               .Select(k => new SelectListItem
               {
                   Text = k.KiyafetAdi,
                   Value = k.Id.ToString()
               });
            // ViewBag --> Controllerdan view katmanina nasil aktaracagimiz kismindaki cozumdur
            ViewBag.kiyafetList = kiyafetList;
            if(id == null || id == 0)
            {
                //ekle 
                return View();
            }
            else
            {
                //guncelle
                SiparisVerme? siparisVerme = _siparisVermeRepository.Get(u => u.Id == id);
                if (siparisVerme == null)
                {
                    return NotFound();
                }
                return View(siparisVerme);

            }
            
        }

        [HttpPost]
        public IActionResult SiparisVermeEkleGuncelle(SiparisVerme siparisVerme)
        {
            //// Kullanici hatalarini frontend de kontrol etme kismi
            //_applicationDbContext.Kiyafetler.Add(kiyafet);
            //// Girilen bilgileri SaveChanges ile db ye kaydetttim
            //_applicationDbContext.SaveChanges();
            //// Index e gonderdim tum kayitlari gorebilmek icin
            //return RedirectToAction("Index");


            //Kullanici hatalarini backend de kontrol etme kismi
            //var errors = ModelState.Values.SelectMany(x => x.Errors);
            if (ModelState.IsValid)
            {
                if (siparisVerme.Id == 0)
                {
                    _siparisVermeRepository.Ekle(siparisVerme);
                    TempData["basarili"] = "Sipariş verme işlemi başarılı bir şekilde gerçekleştirildi.";
                }
                else
                {
                    _siparisVermeRepository.Guncelle(siparisVerme);
                    TempData["basarili"] = "Sipariş başarılı bir şekilde güncellendi.";
                }
                // Girilen bilgileri SaveChanges ile db ye kaydetttim ---> Kaydet olarak degistirdim
                _siparisVermeRepository.Kaydet();
                // TempData ile view-controller, controller-view arasinda verileri tasidim
                
                // Index e gonderdim tum kayitlari gorebilmek icin
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }

        }

        //public IActionResult SepeteEkle(int kiyafetId)
        //{
        //    var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        //    var siparisVerme = _siparisVermeRepository.Get(x => x.kullaniciId == int.Parse(userId) && x.KiyafetId == kiyafetId);

        //    if (siparisVerme == null)
        //    {
        //        var kiyafet = _kiyafetRepository.Get(x => x.Id == kiyafetId);
        //        siparisVerme = new SiparisVerme
        //        {
        //            kullaniciId = int.Parse(userId),
        //            KiyafetId = kiyafetId,
        //            Kiyafet = kiyafet
        //        };
        //        _siparisVermeRepository.Ekle(siparisVerme);
        //    }
        //    else
        //    {
        //        // Eğer aynı kıyafet zaten sepetteyse, miktarını artırabilirsiniz.
        //        // siparisVerme.Miktar++;
        //        _siparisVermeRepository.Guncelle(siparisVerme);
        //    }
        //    _siparisVermeRepository.Kaydet();
        //    return RedirectToAction("Sepet");
        //}

        //public IActionResult Sepet()
        //{
        //    var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        //    var sepetItems = _siparisVermeRepository.GetAll(includeProps: "Kiyafet").Where(x => x.kullaniciId == int.Parse(userId)).ToList();
        //    return View(sepetItems);
        //}

        //public IActionResult SiparisVer()
        //{
        //    var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        //    var sepetItems = _siparisVermeRepository.GetAll(includeProps: "Kiyafet").Where(x => x.kullaniciId == int.Parse(userId)).ToList();

        //    // Bu aşamada siparişleri işleyip veritabanına kaydediyoruz ve sepeti temizliyoruz
        //    foreach (var item in sepetItems)
        //    {
        //        _siparisVermeRepository.Sil(item);
        //    }
        //    _siparisVermeRepository.Kaydet();
        //    TempData["basarili"] = "Siparişler başarılı bir şekilde oluşturuldu.";
        //    return RedirectToAction("Index");
        //}


        //public IActionResult SiparisVerEkleGuncelle(int? id)
        //{
        //    IEnumerable<SelectListItem> kiyafetList = _kiyafetRepository.GetAll()
        //       .Select(k => new SelectListItem
        //       {
        //           Text = k.KiyafetAdi,
        //           Value = k.Id.ToString()
        //       });
        //    // ViewBag --> Controllerdan view katmanina nasil aktaracagimiz kismindaki cozumdur
        //    ViewBag.kiyafetList = kiyafetList;
        //    if (id == null || id == 0)
        //    {
        //        //ekle 
        //        return View();
        //    }
        //    else
        //    {
        //        //guncelle
        //        SiparisVerme? siparisVerme = _siparisVermeRepository.Get(u => u.Id == id);
        //        if (siparisVerme == null)
        //        {
        //            return NotFound();
        //        }
        //        return View(siparisVerme);

        //    }

        //}
        //[HttpPost]
        //public IActionResult SiparisVerEkleGuncelle(Kiyafet kiyafet, int id)
        //{
        //    System.Security.Claims.ClaimsPrincipal currentUser = this.User;
        //    var userId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
        //    if (ModelState.IsValid)
        //    {
        //        SiparisVerme siparisVerme = new SiparisVerme();

        //        siparisVerme.kullaniciId = 0;
        //        siparisVerme.KiyafetId = id;
        //        siparisVerme.Kiyafet = kiyafet;
        //        if (siparisVerme.Id == 0)
        //        {
        //            _siparisVermeRepository.Ekle(siparisVerme);
        //            TempData["basarili"] = "Sipariş verme işlemi başarılı bir şekilde gerçekleştirildi.";
        //        }
        //        else
        //        {
        //            _siparisVermeRepository.Guncelle(siparisVerme);
        //            TempData["basarili"] = "Sipariş başarılı bir şekilde güncellendi.";
        //        }
        //        // Girilen bilgileri SaveChanges ile db ye kaydetttim ---> Kaydet olarak degistirdim
        //        _siparisVermeRepository.Kaydet();
        //        // TempData ile view-controller, controller-view arasinda verileri tasidim

        //        // Index e gonderdim tum kayitlari gorebilmek icin
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        return View();
        //    }
        //    return View("../Home/Index");

        //}

        //public IActionResult KiyafetGuncelle(int? id)
        //{
        //    if(id == null ||id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Kiyafet? kiyafet = _kiyafetRepository.Get(u => u.Id == id);
        //    if( kiyafet == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(kiyafet);
        //}

        //[HttpPost]
        //public IActionResult KiyafetGuncelle(Kiyafet kiyafet)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _kiyafetRepository.Guncelle(kiyafet);
        //        _kiyafetRepository.Kaydet();
        //        TempData["basarili"] = "Kıyafet başarılı bir şekilde güncellendi.";
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        return View();
        //    }

        //}

        public IActionResult SiparisVermeSil(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            SiparisVerme? siparisVerme = _siparisVermeRepository.Get(u => u.Id == id);
            if (siparisVerme == null)
            {
                return NotFound();
            }
            return View(siparisVerme);
        }


        [HttpPost]
        public IActionResult SiparisVermeSil(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            SiparisVerme? siparisVerme = _siparisVermeRepository.Get(u =>u.Id == id);
            if (siparisVerme == null)
            {
                return NotFound();
            }
            _siparisVermeRepository.Sil(siparisVerme);
            _siparisVermeRepository.Kaydet();
            TempData["basarili"] = "Sipariş başarılı bir şekilde silindi.";
            return RedirectToAction("Index");

        }

    }
}
