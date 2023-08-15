using E_Commerce_MVC.Areas.Identity.Data;
using E_Commerce_MVC.Services.Abstract;
using E_Commerce_Shared.DTO;
using E_Commerce_Shared.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_MVC.Controllers
{
    public class ProductClientController : Controller
    {

        private readonly IProductService _productService;
        private readonly IStarService _starService;
        private readonly E_Commerce_MVCContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IFavouriteService _favouriteService;
        public ProductClientController(E_Commerce_MVCContext context, IFavouriteService favouriteService, IStarService starService, UserManager<ApplicationUser> userManager ,IProductService productService)
        {
            _starService = starService;
            _userManager = userManager;
            _productService = productService;   
            _context = context;
            _favouriteService = favouriteService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _productService.ListProduct();
            if (result != null)
            {
                return View(result.Data);
            }
            return View("Error");
        }

        [HttpGet("productById/{productId}")]
        public async Task<IActionResult> GetProductById([FromRoute]int productId)
        {
            var result = await _productService.GetProduct(productId);
            //var resultPro = await _context.Products.Include(x => x.ProductImages).Include(x => x.Features).FirstOrDefaultAsync(x => x.Id == productId);
            if (result.Data != null)
            {
                return View(result.Data);
            }
            return View("Error");
        }

        [HttpGet("GetProducts/{page}")]
        public async Task<IActionResult> GetAll([FromRoute]int page)
        {
            var result = await _productService.GetProducts(page);
            if (result.Data!= null)
            {
                return View(result.Data);
            }
            return View("Error");   
        }

        [HttpGet("GetProductsByCategory/{categoryId}")]
        public async Task<IActionResult> GetProductByCategory([FromRoute]int categoryId)
        {
            var result = await _productService.GetProductsByCategory(categoryId);
            if (result != null)
            {
                return View(result.Data);
            }
            return View("Error");   
        }


        [HttpPost("SendRate/{productId}")]
        public async Task<IActionResult> GiveStar(string star, [FromRoute]int productId)
        {
            var userEmail = HttpContext.User.Identity.Name;
            var user = await _userManager.FindByEmailAsync(userEmail);
            StarDTO _star = new StarDTO();
            double starValue = 0;
            _star.RateStar = double.Parse(star);
            _star.ProductId = productId;
            _star.UserId = user.Id;
            var IsSuccess = await _starService.GiveStarProduct(_star);
            return View(IsSuccess.Message);
        }


        [HttpPost("AddMyFavourites/{productId}")]
        public async Task AddFavourites([FromRoute]int productId)
        {
            var user = HttpContext.User.Identity.Name;
            var userDetail = await _userManager.FindByEmailAsync(user);
            FavouriteDTO model = new FavouriteDTO()
            {
                ProductId = productId,
                UserId = userDetail.Id,
            };
            await _favouriteService.AddMyFavourite(model);
        }

        [HttpDelete("DeleteMyFavourites/{productId}")]
        public async Task DeleteMyFavourite([FromRoute]int productId)
        {
            var user = HttpContext.User.Identity.Name;
            var userDetail = await _userManager.FindByNameAsync(user);
            var favourite = await _context.Favourites.FirstOrDefaultAsync(x => x.ProductId == productId && userDetail.Id == x.UserId);
            await _favouriteService.DeleteMyFavourite(favourite.Id);
        }

        [HttpGet("GetAllMyFavourites")]
        public async Task<List<Favourite>> GetAllMyFavourites()
        {
            var user = HttpContext.User.Identity.Name;
            var userDetail = await _userManager.FindByNameAsync(user);
            var favourites = await _favouriteService.GetAllMyFavourites(userDetail.Id);
            if (favourites.Data != null)
            {
                return favourites.Data;
            }
            return null;
        }

    }
}
