using E_Commerce_Shared;
using E_Commerce_Shared.DTO;
using E_Commerce_Shared.Entity;

namespace E_Commerce_MVC.Services.Abstract
{
    public interface IProductService
    {
        Task<ServiceResponse<ProductDTO>> CreateProduct(ProductDTO model);
        Task<ServiceResponse<bool>> DeleteProduct(int productId);
        Task<ServiceResponse<Product>> GetProduct(int productId);
        Task<ServiceResponse<ProductDTO>> UpdateProduct(int productId, ProductDTO product);
        Task<ServiceResponse<List<Product>>> GetProductsByCategory(int categoryId);
        Task<ServiceResponse<List<Product>>> ListProduct();
        Task<ServiceResponse<ProductSearchResult>> GetProducts(int page);
    }
}
