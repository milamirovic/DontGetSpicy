using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DontGetSpicy.Models
{
     
    public class Potez
    {
       public int ID { get; set; }
       public Igra igra { get; set; }
       public Boja potezOdigrao { get; set; }
        [Range(1,6)]
       public int vrKocke { get; set; } 
       public Figura izabranaFigura { get; set; }
       public DateTime vremeOdigravanja { get; set; }
       
       public Potez()
       {
            
       }
       public Potez(Igra igra, int vrKocke,Boja odigrao)
       {
           this.potezOdigrao=odigrao;
           this.igra=igra;
           this.vremeOdigravanja=DateTime.Now;
           this.vrKocke=vrKocke;
           
       }
       
    
    
    }
    

}