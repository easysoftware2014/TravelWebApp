using System;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace TravelWebApp
{
    [HubName("travelChatHub")]
    public class TravelHub : Hub
    {
        [HubMethodName("globalBroadCast")]
        public void Announce(string message)
        {
            Clients.All.Announce(message);
        }

        public DateTime GetServerDateTime()
        {
            return DateTime.Now;
        }
    }
}