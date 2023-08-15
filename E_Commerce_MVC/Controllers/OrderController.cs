using AutoMapper;
using E_Commerce_MVC.Areas.Identity.Data;
using E_Commerce_MVC.Constants;
using E_Commerce_MVC.Models;
using E_Commerce_MVC.Services.Abstract;
using E_Commerce_Shared.DTO;
using E_Commerce_Shared.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace E_Commerce_MVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;
        public OrderController(UserManager<ApplicationUser> userManager,IOrderService orderService,ICategoryService categoryService,IMapper mapper)
        {
            _mapper = mapper;
            _userManager = userManager;
            _orderService = orderService;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("CheckoutPage")]
        public async Task<IActionResult> GoToCheckout()
        {
            var basket = HttpContext.Session.GetString(Key.Basket_Items);
            var user = HttpContext.Session.GetString(Key.Users);
            var basket_items = JsonConvert.DeserializeObject<List<ProductBasketModel>>(basket);
            var specificUser = JsonConvert.DeserializeObject<ApplicationUser>(user);

            OrderCheckoutModel _model = new OrderCheckoutModel();
            _model.UserId = specificUser.Id;
            _model.FirstName = specificUser.FirstName;  
            _model.LastName = specificUser.LastName;
            _model.OrderDate = DateTime.UtcNow;
            _model.Items = basket_items;
            return View(_model);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCheckout(OrderCheckoutModel model)
        {
            var basket = HttpContext.Session.GetString(Key.Basket_Items);
            var basket_items = JsonConvert.DeserializeObject<List<ProductBasketModel>>(basket);
            var user = HttpContext.User.Identity.Name;
            var userDetail = await _userManager.FindByNameAsync(user);
            List<Product> _product = new List<Product>();
            foreach (var item in basket_items)
            {
                var category = await _categoryService.GetCategoryByName(item.CategoryName);
                Product obj = new Product();
                obj.Id = item.Id;
                obj.ProductName = item.ProductName;
                obj.ProductDescription = item.ProductDescription;
                obj.Price = item.Price;
                obj.ImageUrl = item.ImageUrl;
                obj.CategoryId = category.Id;
                _product.Add(obj);
            }
            var mapDTO = _mapper.Map<OrderDTO>(model);
            mapDTO.UserId = userDetail.Id;
            OrderLastProcessModel _processModel = new OrderLastProcessModel();
            _processModel._product = _product;
            _processModel._orderDTO = mapDTO;

            var orderForSession = JsonConvert.SerializeObject(_processModel);
            HttpContext.Session.SetString(Key.orderProcess, orderForSession);

            return RedirectToAction("Index", "Payment");


        }


        [HttpGet("MyOrders/{userId}")]
        [Authorize]
        public async Task<IActionResult> MyOrderList([FromRoute]string userId)
        {
            var specUserName = HttpContext.User.Identity.Name;
            var user = await _userManager.FindByEmailAsync(specUserName);
            userId = user.Id;
            var my_Orders = await _orderService.MyOrders(userId);
            return View(my_Orders.Data); 
        }


        [HttpGet("OrderConfirm")]
        public IActionResult OrderConfirmation(OrderConfirmationModel model)
        {
            return View(model);
        }
    }
}
