using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        // Kiyafet turunu foreignkey olarak ekledim
        public int KiyafetTuruId { get; set; }
        [ForeignKey("KiyafetTuruId")]
        public KiyafetTuru KiyafetTuru { get; set; }

    }
}
