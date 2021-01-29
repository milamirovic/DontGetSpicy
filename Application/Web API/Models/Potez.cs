using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Web_API.Models
{
    public class Potez
    {
       public int ID { get; set; }
       public Igra igra { get; set; }
       public Korisnik odigrao { get; set; }
        [Range(0,6)]
       public int vrKocke { get; set; } 
       public Figura izabranaFigura { get; set; }
    }

}