using System.ComponentModel.DataAnnotations;

namespace TechCareerMVCFinal.Models
{
    public class KiyafetTuru
    {
        // Id alaninin primary key, Ad alaninin da zorunlu alan oldugunu annotation kullanarak belirttim.
        [Key]
        public int Id { get; set; }

        [Required]
        public string Ad { get; set; }
    }
}
