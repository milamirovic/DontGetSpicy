using System;
using System.Security.Claims;
using System.Threading.Tasks;
using DontGetSpicy.DataProvider;
using DontGetSpicy.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace DontGetSpicy.SignalR
{
    [Authorize]
   public class GameHub:Hub
   {
     private DontGetSpicyContext db;
     public GameHub(DontGetSpicyContext _db)
     {
        db=_db;
     }
       public async Task JoinGameGroup()
        {   
            Korisnik kor=await KorisnikProvider.GetKorisnik(db,Context.User.FindFirstValue("email"));
            await Groups.AddToGroupAsync(Context.ConnectionId, Context.User.FindFirstValue("sub"));
            await Clients.GroupExcept(Context.User.FindFirstValue("sub"),Context.ConnectionId).SendAsync("userJoined",kor.username,Context.User.FindFirstValue("Boja"),kor.slika);
        }
        public async Task LeaveGameHub()
        {        
          await Groups.RemoveFromGroupAsync(Context.ConnectionId,Context.User.FindFirstValue("sub") );
        }
        

       
   }
}