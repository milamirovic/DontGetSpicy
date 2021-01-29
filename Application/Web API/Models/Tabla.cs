using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_API.Models
{
    [NotMapped]
    public class Tabla
    {
        public int ID { get; set; }
        public List<Polje> glavnaPolja { get; set; }
        public Polje startCrveno1 { get; set; }
        public Polje startCrveno2 { get; set; }
        public Polje startCrveno3 { get; set; }
        public Polje startCrveno4 { get; set; }
        public Polje startPlavo1 { get; set; }
        public Polje startPlavo2 { get; set; }
        public Polje startPlavo3 { get; set; }
        public Polje startPlavo4 { get; set; }
        public Polje startZuto1 { get; set; }
        public Polje startZuto2 { get; set; }
        public Polje startZuto3 { get; set; }
        public Polje startZuto4 { get; set; }
        public Polje startZeleno1 { get; set; }
        public Polje startZeleno2 { get; set; }
        public Polje startZeleno3 { get; set; }
        public Polje startZeleno4 { get; set; }




    }
}