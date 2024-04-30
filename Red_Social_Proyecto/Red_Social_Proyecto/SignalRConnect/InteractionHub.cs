using Microsoft.AspNetCore.SignalR;

namespace Red_Social_Proyecto.SignalRConnect
{
    public class InteractionHub : Hub
    {
        public async Task SendInteractionNotification(string message)
        {
            await Clients.All.SendAsync("ReceiveInteractionNotification", message);
        }
    }
}
