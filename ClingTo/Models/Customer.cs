using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClingTo.Models
{
    public class Customer : BaseEntity
    {
        public string FullName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public int ZipCode { get; set; }

        public string Country { get; set; }

        public string Email { get; set; }

        public virtual ICollection<Cart> Carts { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<Request> Requests { get; set; }
    }
}