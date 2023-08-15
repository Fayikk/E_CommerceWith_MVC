using AutoMapper;
using E_Commerce_MVC.Areas.Identity.Data;
using E_Commerce_MVC.Services.Abstract;
using E_Commerce_Shared;
using E_Commerce_Shared.DTO;
using E_Commerce_Shared.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_MVC.Services.Concrete
{
    public class OrderService : IOrderService
    {
        private readonly E_Commerce_MVCContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager; 
        public OrderService(E_Commerce_MVCContext context,IMapper mapper,UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager; 
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<Order>> CreateOrder(OrderDTO model, List<Product> _product)
        {
            ServiceResponse<Order> _serviceResponse = new ServiceResponse<Order>();
            var objDTO = _mapper.Map<Order>(model);
            objDTO.OrderDate = DateTime.UtcNow;
            _context.Orders.Add(objDTO);
            await _context.SaveChangesAsync();
            var insertedOrder = _context.Orders.Single(x => x.OrderId == objDTO.OrderId);
            insertedOrder.Products = new List<Product>();
            if (insertedOrder != null)
            {
                foreach (var item in _product)
                {
                    item.Category = null;
                    insertedOrder.Products.Add(item);
                }
                await _context.SaveChangesAsync();
            }
            _serviceResponse.Message = "Operation is success";
            _serviceResponse.Success = true;
            _serviceResponse.Data = objDTO;
            return _serviceResponse;    
        }

        public async Task<ServiceResponse<Order>> GetOrderById(int id)
        {
            ServiceResponse<Order> _order = new ServiceResponse<Order>();
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _order.Success = true;
                _order.Message = "Process is succes";
                _order.Data = order;
                return _order;
            }
            _order.Message = "Process is fail";
            _order.Success = false;
            return _order;
        }

        public async Task<bool> IsMyProduct(string userId, int productId)
        {
            var IsMyProducts = await _context.Orders.Include(x => x.Products).Where(x => x.UserId == userId).ToListAsync();
            foreach (var item in IsMyProducts)
            {
                foreach (var product in item.Products)
                {
                    if (product.Id == productId)
                    {
                        return true;
                    }
                }
            }
            return false;   
        }

        public async Task<ServiceResponse<List<Order>>> ListOrders()
        {
            ServiceResponse<List<Order>> _order = new ServiceResponse<List<Order>>();
            var result = await _context.Orders.Include(x => x.Products).ToListAsync();
            if (result != null)
            {
                _order.Success = true;
                _order.Message = "Your order is success";
                _order.Data = result;
                return _order;
            }
            return null;
        }

        public async Task<ServiceResponse<List<Order>>> MyOrders(string userId)
        {
            ServiceResponse<List<Order>> _order = new ServiceResponse<List<Order>>();
            var specUser = await _userManager.FindByIdAsync(userId);
            var userOrders = await _context.Orders.Include(x=>x.Products).Where(x=>x.UserId == userId).ToListAsync();
            if (userOrders != null)
            {
                _order.Success = true;
                _order.Data = userOrders;
                return _order;  
            }
            _order.Message = "You dont have any orders";
            _order.Success = false;
            return _order;  
        }
    }
}
