 using Microsoft.AspNetCore.Mvc;
using TechCareerMVCFinal.Data;
using TechCareerMVCFinal.Models;

namespace TechCareerMVCFinal.Controllers
{
    public class KiyafetTuruController : Controller
    {
        // dependecy injenction kullandim -> singleton design pattern

        private readonly IKiyafetTuruRepository _kiyafetTuruRepository;
        public KiyafetTuruController(IKiyafetTuruRepository context)
        {
            _kiyafetTuruRepository = context;
        }
        public IActionResult Index()
        {
            // Index action cagrildigi zaman db ye gidecek ve KiyafetTurlerini getirecek bize bu liste ile.
            List<KiyafetTuru> kiyafetTuruList = _kiyafetTuruRepository.GetAll().ToList();  

            return View(kiyafetTuruList);
        }
        
        public IActionResult KiyafetTuruEkle()
        {
            return View();
        }

        [HttpPost]
        public IActionResult KiyafetTuruEkle(KiyafetTuru kiyafetTuru)
        {
            //// Kullanici hatalarini frontend de kontrol etme kismi
            //_applicationDbContext.KiyafetTurleri.Add(kiyafetTuru);
            //// Girilen bilgileri SaveChanges ile db ye kaydetttim
            //_applicationDbContext.SaveChanges();
            //// Index e gonderdim tum kayitlari gorebilmek icin
            //return RedirectToAction("Index");


            //Kullanici hatalarini backend de kontrol etme kismi
            if (ModelState.IsValid)
            {
                _kiyafetTuruRepository.Ekle(kiyafetTuru);
                // Girilen bilgileri SaveChanges ile db ye kaydetttim ---> Kaydet olarak degistirdim
                _kiyafetTuruRepository.Kaydet();
                // TempData ile view-controller, controller-view arasinda verileri tasidim
                TempData["basarili"] = "Yeni Kıyafet Türü başarılı bir şekilde oluşturuldu.";
                // Index e gonderdim tum kayitlari gorebilmek icin
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }

        }

        public IActionResult KiyafetTuruGuncelle(int? id)
        {
            if(id == null ||id == 0)
            {
                return NotFound();
            }
            KiyafetTuru? kiyafetTuru = _kiyafetTuruRepository.Get(u => u.Id == id);
            if( kiyafetTuru == null)
            {
                return NotFound();
            }
            return View(kiyafetTuru);
        }

        [HttpPost]
        public IActionResult KiyafetTuruGuncelle(KiyafetTuru kiyafetTuru)
        {
            if (ModelState.IsValid)
            {
                _kiyafetTuruRepository.Guncelle(kiyafetTuru);
                _kiyafetTuruRepository.Kaydet();
                TempData["basarili"] = "Kıyafet Türü başarılı bir şekilde güncellendi.";
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }

        }

        public IActionResult KiyafetTuruSil(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            KiyafetTuru? kiyafetTuru = _kiyafetTuruRepository.Get(u => u.Id == id);
            if (kiyafetTuru == null)
            {
                return NotFound();
            }
            return View(kiyafetTuru);
        }


        [HttpPost]
        public IActionResult KiyafetTuruSil(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            KiyafetTuru? kiyafetTuru = _kiyafetTuruRepository.Get(u =>u.Id == id);
            if (kiyafetTuru == null)
            {
                return NotFound();
            }
            _kiyafetTuruRepository.Sil(kiyafetTuru);
            _kiyafetTuruRepository.Kaydet();
            TempData["basarili"] = "Kıyafet Türü başarılı bir şekilde silindi.";
            return RedirectToAction("Index");

        }

    }
}
