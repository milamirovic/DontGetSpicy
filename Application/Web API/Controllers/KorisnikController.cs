using System.Collections.Generic;
using System.Threading.Tasks;
using DontGetSpicy;
using DontGetSpicy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;



namespace DontGetSpicy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KorisnikController : ControllerBase
    {
      
        public DontGetSpicyContext Context { get;set; }

        public KorisnikController(DontGetSpicyContext context)
        {
            Context = context;
        }

        [Route("PreuzmiKorisnike")]
        [HttpGet]
        public async Task<List<Korisnik>> PreuzmiKorisnike()
        {
            return await Context.Korisnici.Include(p=>p.mojeIgre).ToListAsync();
            //return await Context.Korisnici.ToListAsync();
        }

        [Route ("UpisiKorisnika")]
        [HttpPost]
        public async Task UpisiKorisnika([FromBody] Korisnik korisnik)
        {
            Context.Korisnici.Add(korisnik);
            await Context.SaveChangesAsync();
        }

        [Route("IzmeniKorisnika")]
        [HttpPut]
        public async Task IzmeniKorisnika([FromBody] Korisnik korisnik)
        {
            Context.Update<Korisnik>(korisnik);
            await Context.SaveChangesAsync();
            //NAPOMENA: Da bi se izmenio konkretan vrt, moramo da swagger-u ukucamo id zeljenog vrt-a
        }

        [Route("IzbrisiKorisnika/{id}")]
        [HttpDelete]
        public async Task IzbrisiKorisnika(int id){
            var korisnik = await Context.FindAsync<Korisnik>(id);
            Context.Remove(korisnik);
            await Context.SaveChangesAsync();
        }

        /*[Route("UpisiIgricu/{idKorisnika}")]
        [HttpPost]
        public async Task UpisiIgricu(int idKorisnika, [FromBody] Igra igrica)
        {
            var korisnik = await Context.Korisnici.FindAsync(idKorisnika);
            igrica.crveniIgrac = korisnik;
            Context.Igre.Add(igrica);

            await Context.SaveChangesAsync();
            
        }
        */
        
       
       
    }
}
