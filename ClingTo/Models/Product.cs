using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClingTo.Models
{
    public class Product : BaseEntity
    {
        [MinLength(5)]
        [MaxLength(1000)]
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [Url]
        public string ImageUrl { get; set; }
    }
}