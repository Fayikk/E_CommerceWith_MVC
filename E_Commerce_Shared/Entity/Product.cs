using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Shared.Entity
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]

        public string ProductDescription { get; set; }
        [Required]
        [Column(TypeName ="decimal(18,2)")]
        public decimal Price { get; set; }
        public int CategoryId { get; set; } 
        public Category Category { get; set; }
        public string? ImageUrl { get; set; }
        public double? RateAvg { get; set; }    

        public List<ProductImage> ProductImages { get; set; }
        public List<Feature> Features { get; set; }
        public List<Order> Orders { get; set; }
    }
}
