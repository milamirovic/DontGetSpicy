using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_API.Models
{
    public enum statusIgre{
        zavrsena,
        pauzirana,
        uToku
    }
    public class Igra
    {
        public int ID { get; set; }
        public Korisnik crveniIgrac { get; set; }
        public Korisnik plaviIgrac { get; set; }
        public Korisnik zeleniIgrac { get; set; }
        public Korisnik zutiIgrac { get; set; }
        [NotMapped]
        public Tabla tabla { get; set; }
        public string stanjeIgre { get; set; }
        public statusIgre status { get; set; }



    }

}