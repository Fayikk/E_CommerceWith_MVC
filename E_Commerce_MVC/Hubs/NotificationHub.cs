using Microsoft.AspNetCore.SignalR;

namespace E_Commerce_MVC.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task SendNotification(string message)
        {
            await Clients.All.SendAsync("Notification",message);
        }

    }
}
