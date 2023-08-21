using Microsoft.AspNetCore.SignalR;

namespace E_Commerce_MVC.Hubs
{
    public class TotalHub : Hub
    {

        public static int TotalUsers { get; set; } = 0;
        public static int TotalViews { get; set; } = 0;

        public override Task OnConnectedAsync()
        {
            TotalUsers++;
            Clients.All.SendAsync("updateTotalUsers", TotalUsers).GetAwaiter().GetResult();
            return base.OnConnectedAsync();
        }


        public override Task OnDisconnectedAsync(Exception? exception)
        {
            TotalUsers--;
            Clients.All.SendAsync("updateTotalUsers", TotalUsers).GetAwaiter().GetResult();
            return base.OnDisconnectedAsync(exception);
        }

        public async Task<string> NewWindowLoaded(string name)
        {
            TotalViews++;
            await Clients.All.SendAsync("updateTotalViews", TotalViews);
            return $"total views from {name} -{TotalViews}";
        }
    }
}
