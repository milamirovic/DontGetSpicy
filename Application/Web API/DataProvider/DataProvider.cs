
using System.Collections.Generic;
using DontGetSpicy.Models;
using Newtonsoft.Json;

namespace DontGetSpicy.DataProvider
{
    public class DataProvider
    {   
        public static void kreirajIgru(DontGetSpicyContext db, Korisnik igruKreirao)
        {
            Igra novaIgra=new Igra();
            novaIgra.kreatorIgre=igruKreirao;
            novaIgra.status=statusIgre.uToku;
            List<Figura> figure=new List<Figura>(16);
            for(int i=1;i<=16;i=i+4)
            {

                figure.Add(new Figura(-(i),(Boja)(i/4)));
                figure.Add(new Figura(-(i+1),(Boja)(i/4)));
                figure.Add(new Figura(-(i+2),(Boja)(i/4)));
                figure.Add(new Figura(-(i+3),(Boja)(i/4)));
            }
            novaIgra.stanjeIgre=JsonConvert.SerializeObject(figure);
            db.Igre.Add(novaIgra);
            db.SaveChanges();
        }
    }
}