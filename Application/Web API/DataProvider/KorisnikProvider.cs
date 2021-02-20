  
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DontGetSpicy.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DontGetSpicy.DataProvider
{
    public class KorisnikProvider
    {   
        public static async Task<Korisnik> GetKorisnik(DontGetSpicyContext db, string email, string password)
        {
            return await db.Korisnici.Where(Korisnik=>Korisnik.email==email&&Korisnik.password==password).FirstOrDefaultAsync(); 
        }
        public static async Task<Korisnik> GetKorisnik(DontGetSpicyContext db, string email)
        {
            return await db.Korisnici.Where(Korisnik=>Korisnik.email==email).FirstOrDefaultAsync();
        }
        public static async Task<Korisnik> GetKorisnik(DontGetSpicyContext db, int id)
        {
            return await db.Korisnici.Where(Korisnik=>Korisnik.ID==id).FirstOrDefaultAsync();
        }
        public static async Task SnimiKorisnika(DontGetSpicyContext db, Korisnik korisnik)
        {
            db.Korisnici.Update(korisnik);
            await db.SaveChangesAsync();
        }
        public static async Task<List<Igra>> GetKorisnikPauziraneIgre(DontGetSpicyContext db, string email)
        {
            return await db.Igre.Include(Igra=>Igra.kreatorIgre).Where(igra => igra.status==statusIgre.pauzirana&&igra.kreatorIgre.email==email).ToListAsync();
        }







    }
}