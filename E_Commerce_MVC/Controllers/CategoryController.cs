using E_Commerce_MVC.Services.Abstract;
using E_Commerce_Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_MVC.Controllers
{
    [Authorize(Roles ="Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        [HttpGet("Categories")]
        public async Task<IActionResult> Index()
        {
            var result = await _categoryService.ListCategory();

            if (result.Data == null)
            {
                return View(result.Data);
            }
            return View(result.Data);
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryDTO model)
        {
            var result = await _categoryService.CreateCategory(model);
            if (result.Success == true)
            {
                return RedirectToAction("Index","Category");
            }
            return View();
        }

        [HttpGet("CategoryUpdate/{categoryId}")]
        public async Task<IActionResult> UpdateCategory([FromRoute]int categoryId)
        {
            var category = await _categoryService.GetCategory(categoryId);
            if (category == null)
            {
                return View();
            }
            return View(category.Data);
        }

        [HttpPost("Edit/{categoryId}")]
        public async Task<IActionResult> EditCategory([FromRoute]int categoryId,CategoryDTO model)
        {
            var result = await _categoryService.UpdateCategory(categoryId, model);
            if (result.Success == true)
            {
                return RedirectToAction("Index","Category");
            }
            return View();
        }

        [HttpPost("{categoryId}")]
        public async Task<IActionResult> DeleteCategory([FromRoute]int categoryId)
        {
            var result = await _categoryService.DeleteCategory(categoryId);
            if (result.Success == true)
            {
                return RedirectToAction("Index", "Category");
            }
            return BadRequest("Hata oluştu");
        }


    }
}
