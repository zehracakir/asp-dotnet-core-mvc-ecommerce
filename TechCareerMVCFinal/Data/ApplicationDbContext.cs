using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TechCareerMVCFinal.Models;

namespace TechCareerMVCFinal.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<KiyafetTuru> KiyafetTurleri { get; set; }
        public DbSet<Kiyafet> Kiyafetler { get; set; }
        public DbSet<SiparisVerme> SiparisVerme {get; set;}
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    }
}
