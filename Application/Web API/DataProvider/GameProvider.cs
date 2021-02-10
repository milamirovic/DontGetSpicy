
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DontGetSpicy.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Nanoid;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

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
            return await db.Igre.Include(igra=>igra.kreatorIgre)
                                .Where(igra => igra.accessCode==accessCode).FirstOrDefaultAsync();
        }

      
        public static async Task<Igra> NadjiIgruFigure(DontGetSpicyContext db, int idIgre)
        {
            return await db.Igre.Include(igra=>igra.figure).Where(Igra=>Igra.ID==idIgre).FirstOrDefaultAsync();
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

















       /* public static async Task dodajIgraca(DontGetSpicyContext db, IConfiguration config,Korisnik pridruzi, Boja boja, Igra igra)
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
          
        }*/
        
    }
}