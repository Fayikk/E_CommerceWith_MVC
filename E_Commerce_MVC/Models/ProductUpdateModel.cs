using System.ComponentModel.DataAnnotations;

namespace E_Commerce_MVC.Models
{
    public class ProductUpdateModel
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        [MaxLength(60)]
        public string ProductDescription { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile CoverPhoto { get; set; }
        public int CategoryId { get; set; }
    }
}
