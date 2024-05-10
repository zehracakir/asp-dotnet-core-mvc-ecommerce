using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TechCareerMVCFinal.Data;

namespace TechCareerMVCFinal.Models
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _applicationDbContext;
        // bu dbSet uzerinden eriscem bundan sonra dedim
        internal DbSet<T> dbSet;
        // dbSet sayesinde uzun uzun _applicationDbContext.KiyafetTurleri demicem

        public Repository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            this.dbSet = _applicationDbContext.Set<T>();
            // foreign yapısındaki degeri getirmek icin kullandim
            _applicationDbContext.Kiyafetler.Include(k => k.KiyafetTuru).Include(k => k.KiyafetTuruId);
        }

        public void Ekle(T entity)
        {
            dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filtre, string? includeProps = null)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filtre);
            // query den birden fazla kayit gelebilme ihtimaline kars i FirstOrDefault() getirsin deidm.
            if (!string.IsNullOrEmpty(includeProps))
            {
                foreach (var includeProp in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }

            }
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(string? includeProps = null)
        {
            IQueryable<T> query = dbSet;
            if (!string.IsNullOrEmpty(includeProps))
            {
                foreach (var includeProp in includeProps.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
                
            }
            return query.ToList();
        }

        // tek kayit silme
        public void Sil(T entity)
        {
            dbSet.Remove(entity);   
        }

        // birden fazla kayit silme
        public void SilAralik(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }
    }
}
