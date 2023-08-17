using E_Commerce_MVC.Areas.Identity.Data;
using E_Commerce_MVC.Services.Abstract;
using E_Commerce_Shared.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_MVC.Controllers
{
    public class FavouriteController : Controller
    {
        private readonly IFavouriteService _favouriteService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly E_Commerce_MVCContext _context;
        public FavouriteController(IFavouriteService favouriteService,UserManager<ApplicationUser> userManager, E_Commerce_MVCContext context)
        {
            _userManager = userManager;
            _favouriteService = favouriteService;   
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Product> _products = new List<Product>();
            var user = HttpContext.User.Identity.Name;
            var userDetail = await _userManager.FindByNameAsync(user);
            var response = await _favouriteService.GetAllMyFavourite(userDetail.Id);
            foreach (var item in response.Data)
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                _products.Add(product); 
            }
            return View(_products);
        }
    }
}
