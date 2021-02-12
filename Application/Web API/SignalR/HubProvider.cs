using Microsoft.AspNetCore.SignalR;

namespace DontGetSpicy.SignalR
{
    public class HubProvider
    {
        private readonly IHubContext <GameHub> _gameHub;
        public HubProvider(IHubContext <GameHub> hub)
        {
            _gameHub=hub;
        }

        public static void proslediBacenuKocku()
        {
          
        }
    }
}