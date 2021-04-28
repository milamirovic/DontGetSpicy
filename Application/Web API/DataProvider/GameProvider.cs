
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DontGetSpicy.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Nanoid;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;

namespace DontGetSpicy.DataProvider
{
    public class GameProvider
    {   
        public static async Task dodajIgru(DontGetSpicyContext db,Igra novaIgra)
        {
            db.Igre.Add(novaIgra);
            foreach(Figura figura in novaIgra.figure)
            db.Figure.Add(figura);
            await db.SaveChangesAsync();
        }
       
        public static async Task<Igra> NadjiIgru(DontGetSpicyContext db, string accessCode)
        {
            return await db.Igre.Include(igra=>igra.kreatorIgre).Include(Igra=>Igra.figure)
                                .Where(igra => igra.accessCode==accessCode).FirstOrDefaultAsync();
        }
         public static async Task<Igra> NadjiIgruId(DontGetSpicyContext db, string id)
        {
            return await db.Igre.Include(igra=>igra.kreatorIgre)
                                .Where(igra => igra.groupNameGUID==id).FirstOrDefaultAsync();
        }

      
        public static async Task<Igra> NadjiIgruFigure(DontGetSpicyContext db, string id)
        {
            return await db.Igre.Include(igra=>igra.figure).Include(Igra=>Igra.kreatorIgre).Where(Igra=>Igra.groupNameGUID==id).FirstOrDefaultAsync();
        }
        public static async Task dodajPotez(DontGetSpicyContext db,Potez noviPotez)
        {
            db.Potezi.Add(noviPotez);
            await db.SaveChangesAsync();
        }
        public static async Task AzurirajIgru(DontGetSpicyContext db, Igra igra)
        {
            db.Igre.Update(igra);
           await db.SaveChangesAsync();
        }
        public static async Task<Potez> getPoslednjiPotezIgre(DontGetSpicyContext db,Igra igra)
        {
           return await db.Potezi.Include(Potez=>Potez.igra).Where(Potez=>Potez.igra.ID==igra.ID).OrderByDescending(Potez=>Potez.vremeOdigravanja).FirstOrDefaultAsync();
        }
        public static async Task AzurirajPotez(DontGetSpicyContext db, Potez potez)
        {
            db.Potezi.Update(potez);
           await db.SaveChangesAsync();
        }
        public static async Task<List<string>> slikeIgraca(DontGetSpicyContext db, Igra igra)
        {
               List<Korisnik> rez=new List<Korisnik>();
               rez.Add(await KorisnikProvider.GetKorisnik(db,igra.crveniIgracId));
               rez.Add(await KorisnikProvider.GetKorisnik(db,igra.zeleniIgracId));
               rez.Add(await KorisnikProvider.GetKorisnik(db,igra.zutiIgracId));
               rez.Add(await KorisnikProvider.GetKorisnik(db,igra.plaviIgracId));
               return rez.Select(kor =>(kor!=null)?kor.slika:null).ToList();
        }
        public static async Task<Igra> NadjiJavnuIgru(DontGetSpicyContext db, Boja boja,IConfiguration config)
        {
            Igra igraRet;

            SqlConnection conn=new SqlConnection(config.GetConnectionString("DontGetSpicyCS"));
            string availableQuery=$"select top 1 id from Igra where {boja.ToString()}Username is NULL and {boja.ToString()}IgracId=0 and privateGame=0 and Status=2";
            SqlCommand command=new SqlCommand(availableQuery,conn);
            SqlDataReader dataReader;
          
            conn.Open();
            dataReader=await command.ExecuteReaderAsync();
            dataReader.Read();
            if(dataReader.HasRows)
            {
                int id=dataReader.GetInt32(0); 
                igraRet=db.Igre.Find(id);

            }
            else
            {
                igraRet=null;
            }            
            command.Dispose();
            conn.Close();
            return igraRet;

        }
        

















        public static async Task dodajIgraca(DontGetSpicyContext db, IConfiguration config,Korisnik pridruzi, Boja boja, Igra igra)
        {      
             SqlConnection conn=new SqlConnection(config.GetConnectionString("DontGetSpicyCS"));
                string updateQuery=$"UPDATE Igra SET {boja.ToString()}Username='{pridruzi.username}',{boja.ToString()}IgracId={pridruzi.ID} WHERE Igra.ID={igra.ID}";
                SqlCommand command=new SqlCommand(updateQuery,conn);SqlDataAdapter dataAdapter=new SqlDataAdapter();conn.Open();dataAdapter.UpdateCommand=new SqlCommand(updateQuery,conn);
                await dataAdapter.UpdateCommand.ExecuteNonQueryAsync();  command.Dispose();conn.Close();
        }


         public static async Task<bool> slobodnaBoja(IConfiguration config, Boja boja, Igra igra)
        {
            SqlConnection conn=new SqlConnection(config.GetConnectionString("DontGetSpicyCS"));
            string availableQuery=$"select ISNULL(Igra.{boja.ToString()}Username,-1),ISNULL(Igra.{boja.ToString()}IgracId,-1)  from Igra where Igra.ID={igra.ID}";
            SqlCommand command=new SqlCommand(availableQuery,conn);
            SqlDataReader dataReader;
          
            conn.Open();
            dataReader=await command.ExecuteReaderAsync();
            dataReader.Read();
            string bool1=dataReader.GetString(0);  //-1
            int bool2=dataReader.GetInt32(1);  //0
            command.Dispose();
            conn.Close();
        
            if(bool1.CompareTo("-1")==0&&bool2==0)
            {
                return true;
            }
            else
            return false;
          
        }
        
    }
}