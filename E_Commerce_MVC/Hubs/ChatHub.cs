using E_Commerce_MVC.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.SignalR;

namespace E_Commerce_MVC.Hubs
{
    public class ChatHub : Hub
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly E_Commerce_MVCContext _context;
        public ChatHub(UserManager<ApplicationUser> userManager, E_Commerce_MVCContext context)
        {
            _userManager = userManager;
            _context = context;
        }


        public async Task SendMessage(string? senderEmail, string? message, string? returnUser,string? productId)
        {
            var sender = await _userManager.FindByEmailAsync(senderEmail);
            var deneme = int.Parse(productId);
            var product = await _context.Products.FindAsync(deneme); 
            if (senderEmail != "admin@gmail.com")
            {
                var receiver = await _userManager.FindByEmailAsync("admin@gmail.com");

                await Clients.User(receiver.Id.ToString()).SendAsync("ReceiverUser", senderEmail, message,product.ProductName);
                await Clients.User(sender.Id.ToString()).SendAsync("ReceiverUser", senderEmail, message, product.ProductName);
            }
            else
            {
                var receiver = await _userManager.FindByEmailAsync(returnUser);
                await Clients.User(sender.Id.ToString()).SendAsync("ReceiverUser", senderEmail, message,""); 
                await Clients.User(receiver.Id.ToString()).SendAsync("ReceiverUser", senderEmail, message,"");
            }
        }
    }
}
