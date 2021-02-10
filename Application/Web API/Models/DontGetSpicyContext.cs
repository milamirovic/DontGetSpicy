using Microsoft.EntityFrameworkCore;

namespace DontGetSpicy.Models
{
   
    public class DontGetSpicyContext : DbContext
    {
        public DbSet<Korisnik> Korisnici {get;set;}    
        public DbSet<Igra> Igre {get;set;}
        public DbSet<Potez> Potezi {get;set;}
        public DbSet<Figura> Figure {get;set;}
        public DontGetSpicyContext(DbContextOptions options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
        }
    }
}