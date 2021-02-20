using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using DontGetSpicy.DataProvider;
using DontGetSpicy.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace DontGetSpicy.SignalR
{
    [Authorize]
   public class ChatHub:Hub
   {
        private DontGetSpicyContext db;
        public ChatHub(DontGetSpicyContext _db)
        {
            db=_db;
        }
        public async Task StartChat()
        {   
            await Groups.AddToGroupAsync(Context.ConnectionId, Context.User.FindFirstValue("sub"));
        }
        public async Task SendMessage(string message)
        {
            Korisnik korisnik=await KorisnikProvider.GetKorisnik(db,Context.User.FindFirstValue("email"));
            await Clients.GroupExcept(Context.User.FindFirstValue("sub"),Context.ConnectionId).SendAsync("userSentMessage", message, korisnik.username);
        }
        
       
   }
}