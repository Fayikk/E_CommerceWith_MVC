using E_Commerce_MVC.Areas.Identity.Data;
using E_Commerce_MVC.Models;
using E_Commerce_MVC.Services.Abstract;
using E_Commerce_Shared.DTO;
using E_Commerce_Shared.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E_Commerce_MVC.Controllers
{
    [Authorize(Roles ="Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IProductImageService _productImageService;
        private readonly E_Commerce_MVCContext _context;
        private readonly IFeatureService _featureService;
        public ProductController(IProductService productService,IFeatureService featureService ,E_Commerce_MVCContext context, IProductImageService productImageService,ICategoryService categoryService,IWebHostEnvironment webHostEnvironment)
        {
            _productService = productService;
            _categoryService = categoryService;
            _webHostEnvironment = webHostEnvironment;
            _productImageService = productImageService; 
            _context = context; 
            _featureService = featureService;
        }


        [HttpGet("EditProduct/{productId}")]
        public async Task<IActionResult> UpdateProduct([FromRoute]int productId)
        {
            var product = await _productService.GetProduct(productId);
            ProductUpdateModel _model = new ProductUpdateModel();
            _model.ProductId = productId;
            _model.ProductName = product.Data.ProductName;
            _model.ProductDescription = product.Data.ProductDescription;    
            _model.Price = product.Data.Price;  
            _model.ImageUrl = product.Data.ImageUrl;    
            _model.CategoryId = product.Data.CategoryId;
            LoadCategorySelectDataView();
            if (product != null)
            {
                return View(_model);
            }
            else
            {
                return BadRequest(product.Message);
            }

        }


        [HttpPost("ProductEdit/{productId}")]
        public async Task<IActionResult> EditProduct([FromRoute]int productId,ProductUpdateModel model)
        {
            ProductDTO productDTO = new ProductDTO();
            var productForPhoto = await _productService.GetProduct(productId);
            productDTO.ProductDescription = model.ProductDescription;
            productDTO.ProductName = model.ProductName;
            productDTO.Price = model.Price;
            productDTO.ImageUrl = productForPhoto.Data.ImageUrl;   
            productDTO.CategoryId = model.CategoryId;
            if (model.CoverPhoto != null)
            {
                string folder = "Images\\";
                folder += model.CoverPhoto.FileName;
                string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                await model.CoverPhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                productDTO.ImageUrl = model.CoverPhoto.FileName;
            }
            var result = await _productService.UpdateProduct(productId, productDTO);
            if (result.Success == true)
            {
                return RedirectToAction(nameof(ProductList));
            }
            return BadRequest(result.Message);
        }


        [HttpGet]
        
        public async Task<IActionResult> CreateProduct()
        {
            ProductModel model = new ProductModel();
            LoadCategorySelectDataView();
            ViewData["Title"] = "Ürün oluştur";
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                ProductDTO product = new ProductDTO();
                product.ProductDescription = model.ProductDescription;
                product.ProductName = model.ProductName;
                product.Price = model.Price;
                product.CategoryId = model.CategoryId;
                if (model.CoverPhoto != null)
                {
                    string folder = "Images\\";
                    folder += model.CoverPhoto.FileName;
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                    await model.CoverPhoto.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
                }
                product.ImageUrl = model.CoverPhoto.FileName;
                var result = await _productService.CreateProduct(product);
                if (result.Success == true)
                {
                    return RedirectToAction("ProductList", "Product");
                }
            }
            return RedirectToAction("CreateProduct", "Product");
        }


        public async Task<IActionResult> ProductList()
        {
            var result = await _productService.ListProduct();
            if (result.Data != null)
            {
                return View(result.Data);
            }
            return RedirectToAction("CreateProduct", "Product");
        }


        [HttpPost("Delete/{productId}")]
        public async Task<IActionResult> DeleteProduct([FromRoute]int productId)
        {
            var result = await _productService.DeleteProduct(productId);
            if (result.Success == true)
            {
                return RedirectToAction(nameof(ProductList));
            }
            return BadRequest("One error occured");
        }


        [HttpGet("MultipleImage/{productId}")]
        public async Task<IActionResult> MultipleImage(ProductMultipleImage model, [FromRoute]int productId)
        {
            model.ProductId = productId;
            return View(model);
        }


        [HttpPost("CreateMultiple/{productId}")]
        public async Task<IActionResult> CreateMultipleImage(ProductMultipleImage model, int productId)
        {
            List<ProductImageDTO> _productImageDTO = new List<ProductImageDTO>();
            if (model.CoverPhoto != null)
            {
                foreach (var item in model.CoverPhoto)
                {
                    ProductImageDTO _model = new ProductImageDTO(); 
                    string folder = "Images\\";
                    folder += item.FileName;
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                    using (var fileStream = new FileStream(serverFolder,FileMode.Create))
                    {
                        await item.CopyToAsync(fileStream);
                    }
                    _model.ImageName = item.FileName;
                    _productImageDTO.Add(_model);
                }
                List<ProductImage> _productImage = new List<ProductImage>();
                foreach (var item in _productImageDTO)
                {
                    ProductImage productImage = new ProductImage();
                    productImage.ImageName = item.ImageName;
                    _productImage.Add(productImage);
                }
                await _context.ProductImages.AddRangeAsync(_productImage);
                await _context.SaveChangesAsync();
                var result = await _productImageService.CreateMultipleImageProduct(_productImageDTO, productId);
                if (result.Data != null)
                {
                    return RedirectToAction(nameof(ProductList));
                }
             
            }
            return View();
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> CreateFeature([FromRoute]int productId)
        {
            FeatureListModel model = new FeatureListModel();
            model.ProductId = productId;
            return View(model);
        }

        [HttpPost("CreateFeatures/{productId}")]
        public async Task<IActionResult> CreateFeatures(FeatureListModel model,[FromRoute]int productId)
        {
            List<FeautureDTO> features = new List<FeautureDTO>();


            for (var i = 0;i<=model.FeatureName.Length-1;i++)
            {
                FeautureDTO feautureDTO = new FeautureDTO();
                feautureDTO.FeatureName = model.FeatureName[i];
                feautureDTO.ProductId = productId;
                features.Add(feautureDTO);  
            }
            var result = await _featureService.CreateFeatureForProduct(features, productId);
            if (result.Success != false)
            {
                return RedirectToAction(nameof(ProductList));
            }
            return View();
        }




        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Deneme()
        {
            return View();
        }
        private void LoadCategorySelectDataView()
        {
            List<Category> categories = _categoryService.GetCategories();
            List<SelectListItem> selectListItem = categories.Select(x=>new SelectListItem(x.CategoryName,x.Id.ToString())).ToList();
            ViewData["categories"] = selectListItem;
        }
    }
}
