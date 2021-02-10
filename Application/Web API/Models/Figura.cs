using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DontGetSpicy.Models
{
    public enum Boja{
        crveni=0,
        zeleni=1,
        zuti=2,
        plavi=3
    }
   
    public class Figura
    {
        [Key]
        public int ID { get; set; }
        public int index { get; set; }
        public Boja boja{ get; set; }
        public Igra igra { get; set; }
        
        public Figura()
        {
            
        }
        public Figura(int index, Boja boja,Igra igra)
        {
            this.index=index;
            this.boja=boja;
            this.igra=igra;
        }
    }
}