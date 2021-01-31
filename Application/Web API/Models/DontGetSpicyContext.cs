using Microsoft.EntityFrameworkCore;

namespace Web_API.Models
{
   
    public class DontGetSpicyContext : DbContext
    {
        public DbSet<Korisnik> Korisnici {get;set;}    
        public DbSet<Igra> Igre {get;set;}
        public DontGetSpicyContext(DbContextOptions options):base(options)
        {

        }
    }
}