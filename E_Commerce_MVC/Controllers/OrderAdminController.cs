using E_Commerce_MVC.Areas.Identity.Data;
using E_Commerce_MVC.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_MVC.Controllers
{
    [Authorize(Roles ="Admin")]
    public class OrderAdminController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly E_Commerce_MVCContext _context;
        public OrderAdminController(IOrderService orderService, E_Commerce_MVCContext context)
        {
            _context = context;
            _orderService = orderService;
        }

        [HttpGet("OrderSettings")]
        public async Task<IActionResult> Index()
        {
            var result = await _orderService.ListOrders();
            if (result != null )
            {
                return View(result.Data);
            }
            return View(result);
        }


        [HttpPost("ChangeStatus/{orderId}")]
        public async Task<IActionResult> StatusCreate([FromRoute]int orderId,string orderStatus)
        {
            var order = await _orderService.GetOrderById(orderId);
            if (order != null)
            {
                order.Data.Status = orderStatus;
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "OrderAdmin");

            }
            return BadRequest(order.Message);
        }}
}
