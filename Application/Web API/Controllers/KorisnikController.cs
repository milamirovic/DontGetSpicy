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
using System.IO;
using System.Net.Http.Headers;
using System;
using Microsoft.AspNetCore.Http;

namespace DontGetSpicy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KorisnikController : ControllerBase
    {
      
        public DontGetSpicyContext db { get;set; }

        public KorisnikController(DontGetSpicyContext context)
        {
            db = context;
        }
       
        [AllowAnonymous]
        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(JsonElement korisnik)
        {
            Korisnik podaciKorisnika=JsonConvert.DeserializeObject<Korisnik>((korisnik).ToString());
            Korisnik loginKorisnik=await KorisnikProvider.GetKorisnik(db,podaciKorisnika.email,podaciKorisnika.password);
            if(loginKorisnik==null)
            return NotFound();
           
            var tokenStr=JWTGenerator.GenerateLoginToken(loginKorisnik);
          
            return Ok(new {tokenStr=tokenStr});
        }
        [Authorize]
        [Route("PodaciKorisnika")]
        [HttpGet]
        public async Task<IActionResult> PodaciKorisnika()
        {          
            string email=User.FindFirstValue("email");
            if(email==null) return BadRequest();
            Korisnik korisnik=await KorisnikProvider.GetKorisnik(db,email);
            return Ok(new {korisnik=korisnik});
        }
       
        [Authorize]
        [Route("AzurirajSliku")]
        [HttpPut]
        public async Task<IActionResult> AzurirajSliku(IFormFile slika)
        {  
            string email=User.FindFirstValue("email");
            if(email==null) return BadRequest();
            Korisnik korisnik=await KorisnikProvider.GetKorisnik(db, email);

             try
                {
                    var file = Request.Form.Files[0];
                    var folderName = Path.Combine("Resources", "Images");
                    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                    if (file.Length > 0)
                    {
                        string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                            fileName=fileName.Substring(fileName.LastIndexOf('.'));
                            fileName=Guid.NewGuid()+fileName;
                        string fullPath = Path.Combine(pathToSave, fileName);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                        if(korisnik.slika.CompareTo(new Korisnik().slika)!=0)
                        {
                            System.IO.FileInfo oldPic = new FileInfo( Path.Combine(pathToSave,korisnik.slika));
                             oldPic.Delete(); 


                        }


                        korisnik.slika=fileName;
                        await KorisnikProvider.SnimiKorisnika(db,korisnik);
                        return Ok();
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex}");
                }
            
            
        }
    






















         [Authorize]
        [Route("IgreKorisnika")]
        [HttpGet]
        public async Task<IActionResult> IgreKorisnika()
        {  
            //samo pauzirane igre
            string email=User.FindFirstValue("email");
            if(email==null) return BadRequest();
            return Ok(new {igre=await KorisnikProvider.GetKorisnikPauziraneIgre(db,email)});
    
        }

        /* [Route ("UpisiKorisnika")]
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
