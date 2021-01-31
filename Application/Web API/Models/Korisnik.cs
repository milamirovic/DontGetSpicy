using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_API.Models
{
    [Table("Korisnik")]   
    public class  Korisnik
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }

        [Column("Email")]
        public string email { get; set; }

        [Column("Password")]
        [DataType(DataType.Password)]
        public string password { get; set; }
        
        [Column("BrojPobeda")]
        public int brojPobeda { get; set; }

        [Column("Slika")]
        public string slika { get; set; }

        public virtual List<Igra> mojeIgre { get; set; }
    }
}