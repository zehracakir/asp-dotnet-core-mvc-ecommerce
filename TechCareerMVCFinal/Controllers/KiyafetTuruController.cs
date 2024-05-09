 using Microsoft.AspNetCore.Mvc;
using TechCareerMVCFinal.Data;
using TechCareerMVCFinal.Models;

namespace TechCareerMVCFinal.Controllers
{
    public class KiyafetTuruController : Controller
    {
        // dependecy injenction kullandim -> singleton design pattern

        private readonly ApplicationDbContext _applicationDbContext;
        public KiyafetTuruController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public IActionResult Index()
        {
            // Index action cagrildigi zaman db ye gidecek ve KiyafetTurlerini getirecek bize bu liste ile.
            List<KiyafetTuru> kiyafetTuruList = _applicationDbContext.KiyafetTurleri.ToList();  

            return View(kiyafetTuruList);
        }
        
        public IActionResult KiyafetTuruEkle()
        {
            return View();
        }

        [HttpPost]
        public IActionResult KiyafetTuruEkle(KiyafetTuru kiyafetTuru)
        {
            // Kullanici hatalarini frontend de kontrol etme kismi
            _applicationDbContext.KiyafetTurleri.Add(kiyafetTuru);
            // Girilen bilgileri SaveChanges ile db ye kaydetttim
            _applicationDbContext.SaveChanges();
            // Index e gonderdim tum kayitlari gorebilmek icin
            return RedirectToAction("Index");


            // Kullanici hatalarini backend de kontrol etme kismi
            //if (ModelState.IsValid)
            //{
            //    _applicationDbContext.KiyafetTurleri.Add(kiyafetTuru);
            //    // Girilen bilgileri SaveChanges ile db ye kaydetttim
            //    _applicationDbContext.SaveChanges();
            //    // Index e gonderdim tum kayitlari gorebilmek icin
            //    return RedirectToAction("Index");
            //}
            //else
            //{
            //    return View();
            //}

        }
    }
}
