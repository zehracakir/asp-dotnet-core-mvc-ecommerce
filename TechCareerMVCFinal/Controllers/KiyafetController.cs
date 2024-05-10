 using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using TechCareerMVCFinal.Data;
using TechCareerMVCFinal.Models;

namespace TechCareerMVCFinal.Controllers
{
    public class KiyafetController : Controller
    {
        // dependecy injenction kullandim -> singleton design pattern

        private readonly IKiyafetRepository _kiyafetRepository;
        private readonly IKiyafetTuruRepository _kiyafetTuruRepository;
        public KiyafetController(IKiyafetRepository context, IKiyafetTuruRepository kiyafetTuruRepository)
        {
            _kiyafetRepository = context;
            _kiyafetTuruRepository = kiyafetTuruRepository;
        }
        public IActionResult Index()
        {
            // Index action cagrildigi zaman db ye gidecek ve KiyafetTurlerini getirecek bize bu liste ile.
            List<Kiyafet> kiyafetList = _kiyafetRepository.GetAll().ToList();
            return View(kiyafetList);
        }
        
        public IActionResult KiyafetEkle()
        {
            IEnumerable<SelectListItem> kiyafetTuruList = _kiyafetTuruRepository.GetAll()
               .Select(k => new SelectListItem
               {
                   Text = k.Ad,
                   Value = k.Id.ToString()
               });
            // ViewBag --> Controllerdan view katmanina nasil aktaracagimiz kismindaki cozumdur
            ViewBag.kiyafetTuruList = kiyafetTuruList;
            return View();
        }

        [HttpPost]
        public IActionResult KiyafetEkle(Kiyafet kiyafet)
        {
            //// Kullanici hatalarini frontend de kontrol etme kismi
            //_applicationDbContext.Kiyafetler.Add(kiyafet);
            //// Girilen bilgileri SaveChanges ile db ye kaydetttim
            //_applicationDbContext.SaveChanges();
            //// Index e gonderdim tum kayitlari gorebilmek icin
            //return RedirectToAction("Index");


            //Kullanici hatalarini backend de kontrol etme kismi
            if (ModelState.IsValid)
            {
                _kiyafetRepository.Ekle(kiyafet);
                // Girilen bilgileri SaveChanges ile db ye kaydetttim ---> Kaydet olarak degistirdim
                _kiyafetRepository.Kaydet();
                // TempData ile view-controller, controller-view arasinda verileri tasidim
                TempData["basarili"] = "Yeni Kıyafet başarılı bir şekilde oluşturuldu.";
                // Index e gonderdim tum kayitlari gorebilmek icin
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }

        }

        public IActionResult KiyafetGuncelle(int? id)
        {
            if(id == null ||id == 0)
            {
                return NotFound();
            }
            Kiyafet? kiyafet = _kiyafetRepository.Get(u => u.Id == id);
            if( kiyafet == null)
            {
                return NotFound();
            }
            return View(kiyafet);
        }

        [HttpPost]
        public IActionResult KiyafetGuncelle(Kiyafet kiyafet)
        {
            if (ModelState.IsValid)
            {
                _kiyafetRepository.Guncelle(kiyafet);
                _kiyafetRepository.Kaydet();
                TempData["basarili"] = "Kıyafet başarılı bir şekilde güncellendi.";
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }

        }

        public IActionResult KiyafetSil(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Kiyafet? kiyafet = _kiyafetRepository.Get(u => u.Id == id);
            if (kiyafet == null)
            {
                return NotFound();
            }
            return View(kiyafet);
        }


        [HttpPost]
        public IActionResult KiyafetSil(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Kiyafet? kiyafet = _kiyafetRepository.Get(u =>u.Id == id);
            if (kiyafet == null)
            {
                return NotFound();
            }
            _kiyafetRepository.Sil(kiyafet);
            _kiyafetRepository.Kaydet();
            TempData["basarili"] = "Kıyafet başarılı bir şekilde silindi.";
            return RedirectToAction("Index");

        }

    }
}
