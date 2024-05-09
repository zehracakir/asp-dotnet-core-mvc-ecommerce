using TechCareerMVCFinal.Data;

namespace TechCareerMVCFinal.Models
{
    public class KiyafetRepository : Repository<Kiyafet>, IKiyafetRepository
    {
        private ApplicationDbContext _applicationDbContext;
        public KiyafetRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public void Guncelle(Kiyafet kiyafet)
        {
            _applicationDbContext.Update(kiyafet);
        }

        public void Kaydet()
        {
            _applicationDbContext.SaveChanges();
        }
    }
}
