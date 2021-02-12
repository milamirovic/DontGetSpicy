using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DontGetSpicy;
using DontGetSpicy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using DontGetSpicy.DataProvider;
using DontGetSpicy.JWT;
using System.Text.Json;

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
       
        [AllowAnonymous]
        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(object korisnik)
        {
            Korisnik podaciKorisnika=JsonConvert.DeserializeObject<Korisnik>(((JsonElement)korisnik).ToString());
            Korisnik loginKorisnik=await KorisnikProvider.GetKorisnik(Context,podaciKorisnika.email,podaciKorisnika.password);
            if(loginKorisnik==null)
            return NotFound();
           
            var tokenStr=JWTGenerator.GenerateLoginToken(loginKorisnik);
          
            return Ok(new {tokenStr=tokenStr,userData=loginKorisnik});
        }
        [Authorize]
        [Route("PodaciKorisnika")]
        [HttpGet]
        public async Task<IActionResult> PodaciKorisnika()
        {   
            //var identity=HttpContext.User.Identity as ClaimsIdentity;
            //IList<Claim> claims=identity.Claims.ToList();
           // var username=claims[0].Value;
            
            string email=User.FindFirstValue("email");
            if(email==null) return BadRequest();
            return Ok(new {korisnik=await KorisnikProvider.GetKorisnik(Context,email)});
          

        }
        [Authorize]
        [Route("IgreKorisnika")]
        [HttpGet]
        public async Task<IActionResult> IgreKorisnika()
        {  
            //samo pauzirane igre
            string email=User.FindFirstValue("email");
            if(email==null) return BadRequest();
            return Ok(new {igre=await KorisnikProvider.GetKorisnikIgre(Context,email)});
    
        }
    

















         [Route ("UpisiKorisnika")]
        [HttpPost]
        public async Task UpisiKorisnika([FromBody] Korisnik korisnik)
        {
            //provera da li vec postoji
            Context.Korisnici.Add(korisnik);
            await Context.SaveChangesAsync();
        }

        [Route("IzmeniKorisnika")]
        [HttpPut]
        public async Task IzmeniKorisnika([FromBody] Korisnik korisnik)
        {   if(Context.Korisnici.Find(korisnik)!=null)
            {
                Context.Update<Korisnik>(korisnik);
                await Context.SaveChangesAsync();
            }
            
        }
        [Route("PreuzmiKorisnike")]
        [HttpGet]
        public async Task<List<Korisnik>> PreuzmiKorisnike()
        {
            return await Context.Korisnici.Include(p=>p.mojeIgre).ToListAsync();
            //return await Context.Korisnici.ToListAsync();
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
