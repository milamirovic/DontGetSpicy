using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DontGetSpicy.Models
{
    [Table("Korisnik")]   
    public class  Korisnik
    {
        [JsonIgnore]
        [Key]
        [Column("ID")]
        public int ID { get; set; }

        [Column("Email")]
        public string email { get; set; }

        [JsonIgnore]
        [Column("Password")]
        [DataType(DataType.Password)]
        public string password { get; set; }
        
        [Column("BrojPobeda")]
        public int brojPobeda { get; set; }

        [Column("Slika")]
        public string slika { get; set; }="default.png";
        [Column("username")]
        public string username { get; set; }

        public virtual List<Igra> mojeIgre { get; set; }
    }
}