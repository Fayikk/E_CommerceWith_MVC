using AutoMapper;
using E_Commerce_MVC.Areas.Identity.Data;
using E_Commerce_MVC.Services.Abstract;
using E_Commerce_Shared;
using E_Commerce_Shared.DTO;
using E_Commerce_Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_MVC.Services.Concrete
{
    public class DiscountService : IDiscountService
    {
        private readonly E_Commerce_MVCContext _context;
        private readonly IMapper _mapper;
        public DiscountService(E_Commerce_MVCContext context,IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<DiscountDTO>> CheckDiscountCoupon(string couponCode)
        {
            var result = await _context.Discounts.Where(x => x.CouponCode == couponCode && x.IsActive == true && x.DiscountCount > 0).FirstOrDefaultAsync();
            ServiceResponse<DiscountDTO> _response = new ServiceResponse<DiscountDTO>();
            var map = _mapper.Map<DiscountDTO>(result);
            if (result != null)
            {
                _response.Success = true;
                _response.Message = "Coupon Implemented";
                _response.Data = map;
                return _response;
            }
            _response.Success = false;
            _response.Message = "Coupon is not found";
            return _response;
        }

        public async Task<ServiceResponse<DiscountDTO>> CreateDiscount(DiscountDTO discount)
        {
            ServiceResponse<DiscountDTO> response = new ServiceResponse<DiscountDTO>(); 
            var addedObj = _mapper.Map<Discount>(discount);
            _context.Discounts.Add(addedObj);
            if (await _context.SaveChangesAsync()>0)
            {
                response.Success = true;
                response.Message = "Created Process Successfully";
                response.Data = discount;
                return response;
            }
            return null;
        }
    }
}
