using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Web_API.Models
{
    public enum statusIgre{
        zavrsena,
        pauzirana,
        uToku
    }

    [Table("Igra")]
    public class Igra
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }

        //public Tabla tabla { get; set; }
        [Column("StanjeIgre")]
        public string stanjeIgre { get; set; }

        [Column("Status")]
        public string status { get; set; }

        [JsonIgnore]
        public Korisnik crveniIgrac { get; set; }

        public int plaviIgracId { get; set; }

        public int zeleniIgracId { get; set; }

        public int zutiIgracId { get; set; }
    }
}