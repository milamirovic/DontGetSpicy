using System;
using System.Collections.Generic;
using DontGetSpicy.Models;
using Microsoft.AspNetCore.SignalR;

namespace DontGetSpicy.SignalR
{
    public static class GameHubHelper
    {
        public static async System.Threading.Tasks.Task kockaBacenaNotifyAsync(IHubContext<GameHub> ctx,string guid,int vrKocke, bool next)
        {
            await ctx.Clients.Group(guid).SendAsync("kockaBacena",vrKocke,next); 
        }
        public static async System.Threading.Tasks.Task figuraPomerenaNotifyAsync(IHubContext<GameHub> ctx,string guid,List<Tuple<int,int>> potezi, bool final)
        {
            await ctx.Clients.Group(guid).SendAsync("figuraPomerena",potezi,final); 
        }
        public static async System.Threading.Tasks.Task krajIgreNotifyAsync(IHubContext<GameHub> ctx,string guid,Boja? boja)
        {
            await ctx.Clients.Group(guid).SendAsync("krajIgre",boja); 
        }   
        public static async System.Threading.Tasks.Task pauzirajIgruNotifyAsync(IHubContext<GameHub> ctx,string guid)
        {
            await ctx.Clients.Group(guid).SendAsync("pauzirajIgru");
        } 
        public static async System.Threading.Tasks.Task nastaviIgru(IHubContext<GameHub> ctx,string guid)
        {
            await ctx.Clients.Group(guid).SendAsync("nastaviIgru");
        } 
        
        
    }

}
