 using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;
using System.Collections.Generic;
using TechCareerMVCFinal.Data;
using TechCareerMVCFinal.Models;

namespace TechCareerMVCFinal.Controllers
{
    public class SiparisVermeController : Controller
    {
        // dependecy injenction kullandim -> singleton design pattern

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
            // Index action cagrildigi zaman db ye gidecek ve KiyafetTurlerini getirecek bize bu liste ile.
            //List<Kiyafet> kiyafetList = _kiyafetRepository.GetAll().ToList();
            List<SiparisVerme> siparisVermeList = _siparisVermeRepository.GetAll(includeProps:"Kiyafet").ToList();
            return View(siparisVermeList);
        }
        
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
