using E_Commerce_Shared;
using E_Commerce_Shared.DTO;
using E_Commerce_Shared.Entity;

namespace E_Commerce_MVC.Services.Abstract
{
    public interface IProductImageService
    {
        Task<ServiceResponse<List<ProductImage>>> CreateMultipleImageProduct(List<ProductImageDTO> model, int productId);
    }
}
