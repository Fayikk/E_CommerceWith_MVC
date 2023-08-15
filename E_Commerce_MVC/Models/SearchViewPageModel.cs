using E_Commerce_Shared.DTO;

namespace E_Commerce_MVC.Models
{
    public class SearchViewPageModel
    {
        public List<ProductElasticDTO> _listProductModel { get; set; }
        public ProductElasticDTO productElasticDTO { get; set; }    
    }
}
