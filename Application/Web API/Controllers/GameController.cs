using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DontGetSpicy;
using DontGetSpicy.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DontGetSpicy.Controllers
{   
    [ApiController]
    [Route("[controller]")]
    public class DontGetSpicyController : ControllerBase
    {   
      
        public DontGetSpicyContext Context { get;set; }

        public DontGetSpicyController(DontGetSpicyContext context)
        {
            Context = context;
        }
        [Route("test")]
        [HttpGet]
        public void PreuzmiKorisnik2e()
        {
         // Korisnik korisnik=Context.Korisnici.FirstOrDefault();
       //  DontGetSpicy.DataProvider.DataProvider.kreirajIgru(Context,korisnik);
            Igra igra =Context.Igre.FirstOrDefault();
            List<Figura> figuras= JsonConvert.DeserializeObject<List<Figura>>(igra.stanjeIgre);


        }
        
   
    }
}
