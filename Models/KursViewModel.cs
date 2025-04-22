using System.Collections;
using System.ComponentModel.DataAnnotations;
using EntityFrameworkCoreApp.Data;
using Microsoft.AspNetCore.SignalR;

namespace EntityFrameworkCoreApp.Models{
    public class KursViewModel{
        [Key]
        public int KursId { get; set; }
        
        [Required(ErrorMessage ="Kurs başlığı boş veya 50 karakterden fazla olamaz")]
        [StringLength(50,ErrorMessage ="Kurs başlığı boş veya 50 karakterden fazla olamaz")]
        [Display(Name ="Kurs Başlığı")]
        public string? KursBaslik { get; set; }

        [Required(ErrorMessage ="Öğretmen seçimi zorunludur")]
        public int OgretmenId { get; set; }

        public ICollection<KursKayit> KursKayitlari { get; set; }=new List<KursKayit>();
    
    }
}