using System.Threading.Tasks;
using DontGetSpicy.Models;
using Microsoft.AspNetCore.SignalR;

namespace DontGetSpicy.SignalR
{
   public class GameHub:Hub
   {
       public async Task JoinGameGroup(string gameGUID,string username, string boja)
        {   
            await Groups.AddToGroupAsync(Context.ConnectionId, gameGUID);
           await Clients.GroupExcept(gameGUID,Context.ConnectionId).SendAsync("userJoined",username,boja);
        }

       
   }
}