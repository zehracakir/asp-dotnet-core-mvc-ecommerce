using Microsoft.EntityFrameworkCore;
using TechCareerMVCFinal.Models;

namespace TechCareerMVCFinal.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<KiyafetTuru> KiyafetTurleri { get; set; }

    }
}
