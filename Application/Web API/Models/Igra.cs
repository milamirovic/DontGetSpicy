using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DontGetSpicy.Models
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

        [Column("StanjeIgre")]
        public string stanjeIgre { get; set; }

        [Column("Status")]
        public statusIgre status { get; set; }
        [JsonIgnore]
        public Korisnik kreatorIgre { get; set; }
        public int crveniIgracId { get; set; }

        public int plaviIgracId { get; set; }

        public int zeleniIgracId { get; set; }

        public int zutiIgracId { get; set; }

        [NotMapped]
        public List<Figura> figure { get; set; }
    }
}