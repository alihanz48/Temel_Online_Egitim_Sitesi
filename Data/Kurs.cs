using System.Collections;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.SignalR;

namespace EntityFrameworkCoreApp.Data{
    public class Kurs{
        [Key]
        public int KursId { get; set; }

        public string? KursBaslik { get; set; }

        public int OgretmenId { get; set; }
        
        public Ogretmen Ogretmen {get;set;}=null!;

        public ICollection<KursKayit> KursKayitlari { get; set; }=new List<KursKayit>();
    }
}