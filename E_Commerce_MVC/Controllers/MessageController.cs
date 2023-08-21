using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_MVC.Controllers
{
    public class MessageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }



        public IActionResult AdminChatPanel()
        {
            return View();
        }
    }
}
