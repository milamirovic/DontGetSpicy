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
using DontGetSpicy.SignalR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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
        private readonly IHubContext <GameHub> _gameHub;

        public GameController(DontGetSpicyContext context,IConfiguration configuration,IHubContext<GameHub> ctx)
        {
            db = context;
            _config=configuration;
            _gameHub=ctx;
           
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
               if(joinGame.sviPrisutni())
               {
                  joinGame.status=statusIgre.uToku;
               }
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
            bool next=false;
            if(!game.imaLiSeStaOdigrati(bojaIgraca,vrKocke))
            {
                if(vrKocke!=6)
                {   next=true;
                game.naPotezu=(Igra.redosledPoteza.Find(bojaIgraca).Next??Igra.redosledPoteza.First).Value;
                }
                game.aleaIactaEst=!game.aleaIactaEst;
                
            }
            await GameProvider.AzurirajIgru(db,game);
            await _gameHub.Clients.Group(game.groupNameGUID).SendAsync("kockaBacena",vrKocke,next);  
            return Ok();
            
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
            List<Tuple<int,int>> potezi=game.odigrajPotez(izabranaFigura,poslednjiPotez.vrKocke,bojaIgraca);
            if(potezi==null) return NotFound();
            game.aleaIactaEst=!game.aleaIactaEst;
            if(poslednjiPotez.vrKocke!=6)
            game.naPotezu=(Igra.redosledPoteza.Find(bojaIgraca).Next??Igra.redosledPoteza.First).Value; 


            await GameProvider.AzurirajIgru(db,game);

            await GameProvider.AzurirajPotez(db,poslednjiPotez);
             await _gameHub.Clients.Group(game.groupNameGUID).SendAsync("figuraPomerena",potezi,poslednjiPotez.vrKocke!=6); 
             if(game.kraj()!=null)  await _gameHub.Clients.Group(game.groupNameGUID).SendAsync("krajIgre",game.kraj()); 
            
            return Ok();
            
        }


    













       /* [Route("testPomeranja")]
        [HttpGet]
        public List<List<List<Tuple<int,string,int,int>>>> testPomeranja()
        {
            List<List<List<Tuple<int,string,int,int>>>> rez= new List<List<List<Tuple<int,string,int,int>>>>(56);
            for(int i=1;i<=56;i++)
            {
                List<List<Tuple<int,string,int,int>>> lista1=new List<List<Tuple<int,string,int,int>>>(6);
                for(int j=0;j<6;j++)
                {
                    List<Tuple<int,string,int,int>> lista2=new List<Tuple<int,string,int,int>>(4);
                    foreach(Boja b in Enum.GetValues(typeof(Boja)))
                    {
                         lista2.Add(Igra.proveraKretanja(new Figura(i,b,null),j+1));
                  
                    }   

                    lista1.Add(lista2);

                }
                rez.Add(lista1);
                
            }
            return rez;
            
        }*/
    }
}
