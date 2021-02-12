using System.Text.Json;
using System.Threading.Tasks;
using DontGetSpicy.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;

namespace DontGetSpicy.SignalR
{
    
   public class ChatHub:Hub
   {
        public async Task StartChat(string gameGUID)
        {   
            await Groups.AddToGroupAsync(Context.ConnectionId, gameGUID);
            await Clients.GroupExcept(gameGUID,Context.ConnectionId).SendAsync("userEnabledChat");
        }
        public async Task StopChat(string gameGUID, Boja boja)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId,gameGUID);
            await Clients.Group(gameGUID).SendAsync("userDisabledChat", boja);
        }
        public async Task SendMessage(string gameGUID, string message)
        {
           
            await Clients.GroupExcept(gameGUID,Context.ConnectionId).SendAsync("userSentMessage", message);
        }
        
       
   }
}