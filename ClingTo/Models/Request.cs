using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClingTo.Models
{
    public class Request : BaseEntity
    {
        [Required]
        [MinLength(10)]
        public string Description { get; set; }

        [Required]
        [Url]
        public string ReferenceImage { get; set; }

        public virtual Customer Customer { get; set; }
    }
}