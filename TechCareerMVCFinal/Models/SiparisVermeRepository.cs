using TechCareerMVCFinal.Data;

namespace TechCareerMVCFinal.Models
{
    public class SiparisVermeRepository : Repository<SiparisVerme>, ISiparisVermeRepository
    {
        private ApplicationDbContext _applicationDbContext;
        public SiparisVermeRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public void Guncelle(SiparisVerme siparisVerme)
        {
            _applicationDbContext.Update(siparisVerme);
        }

        public void Kaydet()
        {
            _applicationDbContext.SaveChanges();
        }
    }
}
