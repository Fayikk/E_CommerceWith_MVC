using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Shared.Entity
{
    public class Discount
    {
        [Key]
        public int Id { get; set; } 
        public string CouponCode { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal DiscountAmount { get; set; } 
        public int DiscountCount { get; set; }  
        public bool IsActive { get; set; }  
    }
}
