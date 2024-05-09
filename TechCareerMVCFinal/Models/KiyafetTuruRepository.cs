using TechCareerMVCFinal.Data;

namespace TechCareerMVCFinal.Models
{
    public class KiyafetTuruRepository : Repository<KiyafetTuru>, IKiyafetTuruRepository
    {
        private ApplicationDbContext _applicationDbContext;
        public KiyafetTuruRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public void Guncelle(KiyafetTuru kiyafetTuru)
        {
            _applicationDbContext.Update(kiyafetTuru);
        }

        public void Kaydet()
        {
            _applicationDbContext.SaveChanges();
        }
    }
}
