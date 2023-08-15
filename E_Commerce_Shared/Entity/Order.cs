using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Shared.Entity
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime OrderDate { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public string Apartment { get; set; }
        public bool IsShipped { get; set; } = false;
        public string? Status { get; set; } = "Your order is preparing";
        public List<Product> Products { get; set; } = new List<Product>();

    }
}
