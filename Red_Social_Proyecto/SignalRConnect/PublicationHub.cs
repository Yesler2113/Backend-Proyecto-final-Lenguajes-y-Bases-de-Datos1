using Microsoft.AspNetCore.SignalR;

namespace Red_Social_Proyecto.SignalRConnect
{
    public class PublicationHub : Hub
    {
        public async Task NotifyNewPublication(string message)
        {
            await Clients.All.SendAsync("ReceivePublication", message);
        }

    }
}
