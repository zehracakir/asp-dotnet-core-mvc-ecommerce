using Microsoft.AspNetCore.Authorization;
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
        public readonly IWebHostEnvironment _webHostEnvironment;
        public KiyafetController(IKiyafetRepository context, IKiyafetTuruRepository kiyafetTuruRepository, IWebHostEnvironment webHostEnvironment)
        {
            _kiyafetRepository = context;
            _kiyafetTuruRepository = kiyafetTuruRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        [Authorize(Roles = "Admin, Kullanici")]
        public IActionResult Index()
        {
            // Index action cagrildigi zaman db ye gidecek ve KiyafetTurlerini getirecek bize bu liste ile.
            //List<Kiyafet> kiyafetList = _kiyafetRepository.GetAll().ToList();
            List<Kiyafet> kiyafetList = _kiyafetRepository.GetAll(includeProps:"KiyafetTuru").ToList();
            return View(kiyafetList);
        }

        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult KiyafetEkleGuncelle(int? id)
        {
            IEnumerable<SelectListItem> kiyafetTuruList = _kiyafetTuruRepository.GetAll()
               .Select(k => new SelectListItem
               {
                   Text = k.Ad,
                   Value = k.Id.ToString()
               });
            // ViewBag --> Controllerdan view katmanina nasil aktaracagimiz kismindaki cozumdur
            ViewBag.kiyafetTuruList = kiyafetTuruList;
            if(id == null || id == 0)
            {
                //ekle 
                return View();
            }
            else
            {
                //guncelle
                Kiyafet? kiyafet = _kiyafetRepository.Get(u => u.Id == id);
                if (kiyafet == null)
                {
                    return NotFound();
                }
                return View(kiyafet);

            }
            
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult KiyafetEkleGuncelle(Kiyafet kiyafet, IFormFile? file)
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
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string kiyafetPath = Path.Combine(wwwRootPath, @"img");

                if(file != null)
                {
                    using (var fileStream = new FileStream(Path.Combine(kiyafetPath, file.FileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    kiyafet.ResimUrl = @"\img\" + file.FileName;
                }
                
                if (kiyafet.Id == 0)
                {
                    _kiyafetRepository.Ekle(kiyafet);
                    TempData["basarili"] = "Yeni Kıyafet başarılı bir şekilde oluşturuldu.";
                }
                else
                {
                    _kiyafetRepository.Guncelle(kiyafet);
                    TempData["basarili"] = "Kıyafet başarılı bir şekilde güncellendi.";
                }
                // Girilen bilgileri SaveChanges ile db ye kaydetttim ---> Kaydet olarak degistirdim
                _kiyafetRepository.Kaydet();
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

        [Authorize(Roles = UserRoles.Role_Admin)]
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
        [Authorize(Roles = UserRoles.Role_Admin)]
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
