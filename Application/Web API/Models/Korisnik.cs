using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web_API.Models
{
   
    public class  Korisnik
    {
        public int ID { get; set; }
        public string email { get; set; }
        [DataType(DataType.Password)]
        public string password { get; set; }
        public List<Igra> mojeIgre { get; set; }
        public int brojPobeda { get; set; }
        public string slika { get; set; }
    }
}