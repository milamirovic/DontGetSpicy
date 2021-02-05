using System.ComponentModel.DataAnnotations.Schema;

namespace DontGetSpicy.Models
{
    public enum Boja{
        crvena=0,
        zelena=1,
        zuta=2,
        plava=3
    }
   [NotMapped]
    public class Figura
    {
        public int index { get; set; }
        public Boja boja{ get; set; }
        
        public Figura(int index, Boja boja)
        {
            this.index=index;
            this.boja=boja;
        }
    }
}