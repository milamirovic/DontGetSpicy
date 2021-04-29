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
using Microsoft.Extensions.Configuration;

namespace DontGetSpicy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KorisnikController : ControllerBase
    {
      
        private DontGetSpicyContext db { get;set; }
        private readonly IConfiguration _config;

        public KorisnikController(DontGetSpicyContext context,IConfiguration c)
        {
            db = context;
            _config=c;
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
                            System.IO.FileInfo oldPic = new FileInfo( Path.Combine(Directory.GetCurrentDirectory(),korisnik.slika));
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
        [Route("MyPausedGames")]
        [HttpGet]
        public async Task<IActionResult> IgreKorisnika()
        {  
            //samo pauzirane igre
            string email=User.FindFirstValue("email");
            if(email==null) return BadRequest();
            return Ok(new {igre=await KorisnikProvider.GetKorisnikPauziraneIgre(db,email)});
    
        }
        [AllowAnonymous]
        [Route("Signup")]
        [HttpPost]
        public async Task<IActionResult> Signup(JsonElement korisnik)
        {
            Korisnik podaciKorisnika=JsonConvert.DeserializeObject<Korisnik>((korisnik).ToString());
            if(await DataProvider.KorisnikProvider.postojiKorisnik(db,podaciKorisnika.username,podaciKorisnika.password))
            return Forbid();
            await DataProvider.KorisnikProvider.dodajKorisnika(db,podaciKorisnika);
            
          
            return Ok();
        }

        
        
       
       
    }
}
