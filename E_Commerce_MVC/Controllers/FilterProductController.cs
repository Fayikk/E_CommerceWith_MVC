using E_Commerce_MVC.Models;
using E_Commerce_MVC.Services.Abstract;
using E_Commerce_Shared.DTO;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_MVC.Controllers
{
    public class FilterProductController : Controller
    {
        private readonly IFilterProductService _filterProductService;
        public FilterProductController(IFilterProductService filterProductService)
        {
            _filterProductService = filterProductService;
        }

        [HttpGet("QueryProducts")]
        public async Task<IActionResult> Search([FromQuery]ProductElasticDTO productElasticDTO)
        {
            SearchViewPageModel _pageModel = new SearchViewPageModel();
            var eCommerceList = await _filterProductService.SearchAsync(productElasticDTO);
            _pageModel._listProductModel = eCommerceList;
            return View(_pageModel);
        }
    }
}
