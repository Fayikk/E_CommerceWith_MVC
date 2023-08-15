using E_Commerce_Shared.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace E_Commerce_Shared.DTO
{
    public class ProductElasticDTO
    {
        [JsonPropertyName("_id")]
        public string Id { get; set; } = null!;
        [JsonPropertyName("productid")]

        public int ProductId { get; set; }
        [JsonPropertyName("product_name")]

        public string ProductName { get; set; } = null!;
        [JsonPropertyName("product_description")]

        public string ProductDescription { get; set; } = null!;
        [JsonPropertyName("price")]

        public double Price { get; set; }
        [JsonPropertyName("categoryid")]

        public int CategoryId { get; set; }
        [JsonPropertyName("_iimage_url")]

        public string? ImageUrl { get; set; } = null!;

        public double? LtePrice { get; set; }   
      
    }
}
