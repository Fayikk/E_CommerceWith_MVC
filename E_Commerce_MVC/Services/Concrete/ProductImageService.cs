using E_Commerce_MVC.Areas.Identity.Data;
using E_Commerce_MVC.Services.Abstract;
using E_Commerce_Shared;
using E_Commerce_Shared.DTO;
using E_Commerce_Shared.Entity;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_MVC.Services.Concrete
{
    public class ProductImageService : IProductImageService
    {
        private readonly E_Commerce_MVCContext _context;
        public ProductImageService(E_Commerce_MVCContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<ProductImage>>> CreateMultipleImageProduct(List<ProductImageDTO> model, int productId)
        {
            ServiceResponse<List<ProductImage>> _productImage = new ServiceResponse<List<ProductImage>>();
            List<ProductImage> productImage = new List<ProductImage>();
            var result = await _context.Products.Where(x => x.Id == productId).Include(x => x.ProductImages).FirstOrDefaultAsync();
            
            foreach (var item in model)
            {
                ProductImage request = new ProductImage();
                request.ImageName = item.ImageName;
                productImage.Add(request);
            }

            if (model.Count != 0)
            {
                _context.ProductImages.AddRange(productImage);
                await _context.SaveChangesAsync();  
                foreach(var image in productImage)
                {
                    var insertedImage = _context.ProductImages.Single(x => x.Id == image.Id);
                    result.ProductImages.Add(insertedImage);
                    await _context.SaveChangesAsync();
                }
                _productImage.Data = productImage;
                _productImage.Success = true;
                return _productImage;

            }
            return null;

        }
    }
}
