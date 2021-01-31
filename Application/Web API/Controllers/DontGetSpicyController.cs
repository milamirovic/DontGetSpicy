using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Web_API.Models;

namespace Web_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DontGetSpicyController : ControllerBase
    {
        private readonly ILogger<DontGetSpicyController> _logger;
        public DontGetSpicyContext Context { get;set; }

        public DontGetSpicyController(DontGetSpicyContext context)
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

        [Route("UpisiIgricu/{idKorisnika}")]
        [HttpPost]
        public async Task UpisiIgricu(int idKorisnika, [FromBody] Igra igrica)
        {
            var korisnik = await Context.Korisnici.FindAsync(idKorisnika);
            igrica.crveniIgrac = korisnik;
            Context.Igre.Add(igrica);

            await Context.SaveChangesAsync();
            
        }
    }
}
