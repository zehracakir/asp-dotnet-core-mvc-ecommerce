using System.ComponentModel.DataAnnotations;

namespace TechCareerMVCFinal.Models
{
    public class Kiyafet
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string KiyafetAdi { get; set; }

        [Required]
        public double Fiyati { get; set; }

        [Required]
        public string Rengi { get; set; }

        [Required]
        public string Cinsiyet { get; set; }
    }
}
