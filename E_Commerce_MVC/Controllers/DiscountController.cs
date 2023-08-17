using E_Commerce_MVC.Areas.Identity.Data;
using E_Commerce_MVC.Constants;
using E_Commerce_MVC.Models;
using E_Commerce_MVC.Services.Abstract;
using E_Commerce_Shared;
using E_Commerce_Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace E_Commerce_MVC.Controllers
{
    public class DiscountController : Controller
    {
        private readonly IDiscountService _service;
        public DiscountController(IDiscountService service)
        {
            _service = service; 
        }

        [HttpGet]
        public async Task<IActionResult> CreateDiscount()
        {
            DiscountDTO model = new DiscountDTO();  
            return View(model);
        }

        [HttpPost("CreateDiscountCode")]
        public async Task<IActionResult> CreateDiscount(DiscountDTO model)
        {
            var result = await _service.CreateDiscount(model);
            if (result.Data != null)
            {
                return RedirectToAction("CreateDiscount", "Discount");
            }
            return View(result);    
        }


        [HttpGet]
        public async Task<ServiceResponse<DiscountDTO>> CheckCoupon(string CouponCode)
        {
            var result = await _service.CheckDiscountCoupon(CouponCode);
            List<ProductBasketModel> _productModel = new List<ProductBasketModel>();
            if (result.Data != null)
            {
                var basket = HttpContext.Session.GetString(Key.Basket_Items);
                var basket_items = JsonConvert.DeserializeObject<List<ProductBasketModel>>(basket);

                foreach (var item in basket_items)
                {
                    item.Price = item.Price - ((item.Price * result.Data.DiscountAmount) / 100);
                    _productModel.Add(item);
                }
                string serializeObject = JsonConvert.SerializeObject(_productModel);
                HttpContext.Session.SetString(Key.Basket_Items, serializeObject);
                return result;
            }
            else
            {
                return result;
            }
            
        }
    }
}
