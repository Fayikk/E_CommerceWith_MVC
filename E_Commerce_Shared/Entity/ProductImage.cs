using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Shared.Entity
{
    public class ProductImage
    {
        [Key]
        public int Id { get; set; }
        public string ImageName { get; set; }
        public List<Product> Products { get; set; }
    }
}
