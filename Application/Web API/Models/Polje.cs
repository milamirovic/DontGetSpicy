using System.ComponentModel.DataAnnotations.Schema;

namespace Web_API.Models
{
    public enum Figura{
        plava,
        crvena,
        zuta,
        zelena,
        prazno
    }
   [NotMapped]
    public class Polje
    {
        public int ID { get; set; }
        public Figura figura{ get; set; }
        
    }
}