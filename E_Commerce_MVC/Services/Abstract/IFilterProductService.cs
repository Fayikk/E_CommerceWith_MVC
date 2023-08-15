using E_Commerce_Shared.DTO;

namespace E_Commerce_MVC.Services.Abstract
{
    public interface IFilterProductService
    {
        Task<List<ProductElasticDTO>> SearchAsync(ProductElasticDTO productViewModel);
    }
}
