using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Shared.DTO
{
    public class DiscountDTO
    {
        public string CouponCode { get; set; }
        public decimal DiscountAmount { get; set; }
        public int DiscountCount { get; set; }
        public bool IsActive { get; set; }

    }
}
