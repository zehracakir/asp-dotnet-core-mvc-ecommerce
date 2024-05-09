using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TechCareerMVCFinal.Models
{
    public class KiyafetTuru
    {
        // Id alaninin primary key, Ad alaninin da zorunlu alan oldugunu annotation kullanarak belirttim.
        [Key]
        public int Id { get; set; }

        // db ye bos bir kayit gonderilmeye calisilirsa engelleyip, kendi error mesajimi ekledim
        [Required (ErrorMessage = "Kıyafet Türü Adı boş bırakılamaz!")]
        //Ad kullandigimda labellarda vs butunluk olmasi acisindan displayname kullandim
        [DisplayName("Kıyafet Türü Adı")]
        [MaxLength(25)] // 25 karakterden fazla olmasini engelledim
        public string Ad { get; set; }
    }
}
