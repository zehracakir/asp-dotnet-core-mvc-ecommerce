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
    }
}
