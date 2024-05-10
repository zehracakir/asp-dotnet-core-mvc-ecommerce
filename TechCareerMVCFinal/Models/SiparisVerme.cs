using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechCareerMVCFinal.Models
{
    public class SiparisVerme
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int kullaniciId { get; set; }

        [ValidateNever]
        public int KiyafetId { get; set; }
        [ForeignKey("KiyafetId")]
        
        [ValidateNever]
        public Kiyafet Kiyafet { get; set; }

    }
}
