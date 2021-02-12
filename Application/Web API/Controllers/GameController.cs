using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DontGetSpicy;
using DontGetSpicy.DataProvider;
using DontGetSpicy.JWT;
using DontGetSpicy.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace DontGetSpicy.Controllers
{  
   
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {   
        private DontGetSpicyContext db { get;set; }
        private static IConfiguration _config;


        public GameController(DontGetSpicyContext context,IConfiguration configuration)
        {
            db = context;
            _config=configuration;
           
        }
        [Authorize]
        [Route("NewGame")]
        [HttpGet]
        public async Task<IActionResult> NovaIgra(Boja boja)
        {
            string email=User.FindFirstValue("email");
            Korisnik korisnik=await KorisnikProvider.GetKorisnik(db,email);
            if(korisnik==null) return BadRequest();
            Igra novaIgra=new Igra(korisnik);
            await GameProvider.dodajIgru(db,novaIgra);
            IActionResult res=(await this.PridruziSeIgri(boja,novaIgra.accessCode));
            
            if( res is OkObjectResult)
            {
                OkObjectResult result=(OkObjectResult) res;
                dynamic val=result.Value;
                return Ok(new {token=val.token,accessCode=novaIgra.accessCode, username=korisnik.username, guid=novaIgra.groupNameGUID});
            }
            return BadRequest();
        }
        [Authorize]
        [Route("JoinGame")]
        [HttpGet]
        public async Task<IActionResult> PridruziSeIgri(Boja boja, string accessCode)
        {
            string email=User.FindFirstValue("email");
            Korisnik korisnik=await KorisnikProvider.GetKorisnik(db,email);
            if(korisnik==null) return BadRequest();
            Igra joinGame=await GameProvider.NadjiIgru(db,accessCode);
            if(joinGame==null) return NotFound();
            if(joinGame.status!=statusIgre.cekanjeIgraca) return Forbid();
            
            if(joinGame.slobodnaBoja(boja))
            {
               joinGame.dodajIgraca(boja,korisnik);
               await GameProvider.AzurirajIgru(db,joinGame);
               return Ok(new {token=JWTGenerator.GenerateGameToken(korisnik,joinGame,boja),username=korisnik.username,guid=joinGame.groupNameGUID,igraci=joinGame.vratiIgrace()});
            }
            else return Forbid();
        }
        [Authorize]
        [Route("ThrowCube")]
        [HttpGet]
        public async Task<IActionResult> BaciKocku()
        {
            
            string igraId=User.FindFirstValue("sub");
            Boja bojaIgraca= Enum.Parse<Boja>(User.FindFirstValue("Boja"));
            Igra game=await GameProvider.NadjiIgruFigure(db,Int32.Parse(igraId));
            if(game==null) return NotFound();

            if(game.status!=statusIgre.uToku||game.naPotezu!=bojaIgraca||game.aleaIactaEst) return Forbid();
            int vrKocke=Igra.generisiKocku();
            Potez noviPotez=new Potez(game,vrKocke,bojaIgraca);
            await GameProvider.dodajPotez(db,noviPotez);
            game.aleaIactaEst=!game.aleaIactaEst;
            if(!game.imaLiSeStaOdigrati(bojaIgraca,vrKocke))
            {
                if(vrKocke!=6)
                {
                game.naPotezu=(Igra.redosledPoteza.Find(bojaIgraca).Next??Igra.redosledPoteza.First).Value;
                }
                game.aleaIactaEst=!game.aleaIactaEst;
                
            }
            await GameProvider.AzurirajIgru(db,game);
            
            return Ok(new {kocka=vrKocke});
            
        }
        [Authorize]
        [Route("MoveFigure")]
        [HttpGet]
        public async Task<IActionResult> OdigrajPotez(int figuraIndex)
        {
            string igraId=User.FindFirstValue("sub");
            Boja bojaIgraca= Enum.Parse<Boja>(User.FindFirstValue("Boja"));
            Igra game=await GameProvider.NadjiIgruFigure(db,Int32.Parse(igraId));
            if(game==null) return NotFound();

            if(game.status!=statusIgre.uToku||game.naPotezu!=bojaIgraca||!game.aleaIactaEst) return Forbid();
            
            Potez poslednjiPotez=await GameProvider.getPoslednjiPotezIgre(db,game);
            if(poslednjiPotez.potezOdigrao!=bojaIgraca) return NotFound();
            Figura izabranaFigura=game.figure.Where(Figura=>Figura.index==figuraIndex&&Figura.boja==bojaIgraca).FirstOrDefault();
            poslednjiPotez.izabranaFigura=izabranaFigura;
            if(izabranaFigura==null)return NotFound();
            if(!game.odigrajPotez(izabranaFigura,poslednjiPotez.vrKocke,bojaIgraca)) return NotFound();
            game.aleaIactaEst=!game.aleaIactaEst;
            if(poslednjiPotez.vrKocke!=6)
            game.naPotezu=(Igra.redosledPoteza.Find(bojaIgraca).Next??Igra.redosledPoteza.First).Value; 


            await GameProvider.AzurirajIgru(db,game);

            await GameProvider.AzurirajPotez(db,poslednjiPotez);
            
            return Ok();
            
        }
        

























        
        [Route("test")]
        [HttpGet]
        public void PreuzmiKorisnik2e()
        {
         // Korisnik korisnik=Context.Korisnici.FirstOrDefault();
       //  DontGetSpicy.DataProvider.DataProvider.kreirajIgru(Context,korisnik);
           // Igra igra =db.Igre.FirstOrDefault();
           // List<Figura> figuras= JsonConvert.DeserializeObject<List<Figura>>(igra.stanjeIgre);


        }
      
        
       
        
   
    }
}
