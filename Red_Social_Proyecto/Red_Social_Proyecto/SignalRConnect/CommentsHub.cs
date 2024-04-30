using Microsoft.AspNetCore.SignalR;

namespace Red_Social_Proyecto.SignalRConnect
{
    public class CommentsHub : Hub
    {
        public async Task SendCommentNotification(string message)
        {
            await Clients.All.SendAsync("ReceiveCommentNotification", message);
        }
    }
}
