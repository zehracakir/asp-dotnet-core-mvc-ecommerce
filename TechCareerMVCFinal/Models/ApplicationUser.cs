using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TechCareerMVCFinal.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string KullaniciAdi;

        [Required]
        public string? Adres {  get; set; }

    }
}
