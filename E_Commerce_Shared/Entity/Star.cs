using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Shared.Entity
{
    public class Star
    {
        [Key]
        public int Id { get; set; } 
        public int? ProductId { get; set; } 
        public string? UserId { get; set; }
        public double? RateStar { get; set; }
    }
}
