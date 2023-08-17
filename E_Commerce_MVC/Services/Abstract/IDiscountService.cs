using E_Commerce_Shared;
using E_Commerce_Shared.DTO;

namespace E_Commerce_MVC.Services.Abstract
{
    public interface IDiscountService
    {
        Task<ServiceResponse<DiscountDTO>> CreateDiscount(DiscountDTO discount);
        Task<ServiceResponse<DiscountDTO>> CheckDiscountCoupon(string couponCode);
    }
}
