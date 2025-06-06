using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkCoreApp.Data{
    public class Ogretmen{

        [Key]
        public int OgretmenId { get; set; }
        public string? Ad { get; set; }
        public string? Soyad { get; set; }

        public String? AdSoyad{get{
            return this.Ad+" "+this.Soyad;
        }}
        
        public string? Telefon { get; set; }
        public string? Eposta { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode =true)]
        public DateTime? BaslamaTarihi { get; set;}
        
        public ICollection<Kurs> Kurslar { get; set; }=new List<Kurs>();
    }
}