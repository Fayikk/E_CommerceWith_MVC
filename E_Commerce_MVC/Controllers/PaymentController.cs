using E_Commerce_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System;
using Iyzipay;
using Iyzipay.Request;
using Iyzipay.Model;
using E_Commerce_MVC.Constants;
using Microsoft.AspNetCore.Identity;
using E_Commerce_MVC.Areas.Identity.Data;
using Newtonsoft.Json;
using E_Commerce_MVC.Services.Abstract;

namespace E_Commerce_MVC.Controllers
{
    public class PaymentController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOrderService _orderService;
        public PaymentController(UserManager<ApplicationUser> userManager,IOrderService orderService)
        {
            _orderService = orderService;
            _userManager = userManager; 
        }

        [HttpGet("PaymentPage")]
        public IActionResult Index()
        {
            PaymentModel model = new PaymentModel();    
            return View(model);
        }

        public async Task<IActionResult> CreatePayment(PaymentModel model)
        {


            var spec_user = HttpContext.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(spec_user);

            Options options = new Options();
            options.ApiKey = Key.apiKey;
            options.SecretKey = Key.secretKet;
            options.BaseUrl = "https://sandbox-api.iyzipay.com";


            var basket = HttpContext.Session.GetString(Key.Basket_Items);
            var basket_Items = JsonConvert.DeserializeObject<List<ProductBasketModel>>(basket);
            decimal totalPrice = 0;
            foreach (var item in basket_Items)
            {
                totalPrice += item.Price;
            }



            CreatePaymentRequest request = new CreatePaymentRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId =Guid.NewGuid().ToString();
            request.Price = totalPrice.ToString().Replace(",", ".");
            request.PaidPrice = totalPrice.ToString().Replace(",", ".");
            request.Currency = Currency.TRY.ToString();
            request.Installment = 1;
            request.BasketId = "B67832";
            request.PaymentChannel = PaymentChannel.WEB.ToString();
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

            PaymentCard paymentCard = new PaymentCard();
            paymentCard.CardHolderName = model.CardHolderName;
            paymentCard.CardNumber = model.CardNumber;
            paymentCard.ExpireMonth = model.ExpireMonth;
            paymentCard.ExpireYear =model.ExpireYear ;
            paymentCard.Cvc = model.Cvc;
            paymentCard.RegisterCard = 0;
            request.PaymentCard = paymentCard;

            Buyer buyer = new Buyer();
            buyer.Id = user.Id;
            buyer.Name = user.FirstName;
            buyer.Surname = user.LastName;
            buyer.GsmNumber = user.PhoneNumber;
            buyer.Email =user.Email;
            buyer.IdentityNumber = "74300864791";
            buyer.LastLoginDate = "";
            buyer.RegistrationDate = "";
            buyer.RegistrationAddress = "Denneme";
            buyer.Ip = "85.34.78.112";
            buyer.City = "Istanbul";
            buyer.Country = "Turkey";
            buyer.ZipCode = "34732";
            request.Buyer = buyer;

            Address shippingAddress = new Address();
            shippingAddress.ContactName = "Jane Doe";
            shippingAddress.City = "Istanbul";
            shippingAddress.Country = "Turkey";
            shippingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            shippingAddress.ZipCode = "34742";
            request.ShippingAddress = shippingAddress;


            Address billingAddress = new Address();
            billingAddress.ContactName = "Jane Doe";
            billingAddress.City = "Istanbul";
            billingAddress.Country = "Turkey";
            billingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            billingAddress.ZipCode = "34742";
            request.BillingAddress = billingAddress;

            List<BasketItem> basketItems = new List<BasketItem>();

            foreach (var item in basket_Items)
            {
                BasketItem firstBasketItem = new BasketItem();
                firstBasketItem.Id = item.Id.ToString();
                firstBasketItem.Name = item.ProductName;
                firstBasketItem.Category1 = item.CategoryName;
                firstBasketItem.ItemType = BasketItemType.PHYSICAL.ToString();
                firstBasketItem.Price = item.Price.ToString().Replace(",",".");

                basketItems.Add(firstBasketItem);
            }
            OrderConfirmationModel confirmModel = new OrderConfirmationModel();


            request.BasketItems = basketItems;  
            Payment payment = Payment.Create(request, options);
            confirmModel.OrderNumber = request.ConversationId;
            confirmModel.OrderDate = DateTime.UtcNow;
            confirmModel.TotalAmount = totalPrice;


            OrderLastProcessModel orderDes = new OrderLastProcessModel();
            if (payment.Status == "success")
            {
                HttpContext.Session.Remove(Key.Basket_Items);
                var order = HttpContext.Session.GetString(Key.orderProcess);
                orderDes = JsonConvert.DeserializeObject<OrderLastProcessModel>(order);
                await _orderService.CreateOrder(orderDes._orderDTO, orderDes._product);
                return RedirectToAction("OrderConfirmation", "Order",confirmModel);
            }
            return null;
        }



    }
}
