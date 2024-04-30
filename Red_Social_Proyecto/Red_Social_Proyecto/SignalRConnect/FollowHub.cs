using Microsoft.AspNetCore.SignalR;

namespace Red_Social_Proyecto.SignalRConnect
{
    public class FollowHub : Hub
    {

        public async Task SendFollowNotification(string message)
        {
            await Clients.All.SendAsync("ReceiveFollowNotification", message);
        }
    }
}
