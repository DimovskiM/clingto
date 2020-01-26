using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClingTo.Models
{
    public class Cart : BaseEntity
    {
        public decimal TotalPrice { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public void UpdateTotalPrice()
        {
            TotalPrice = Products.Select(x => x.Price).Sum();
        }
    }
}