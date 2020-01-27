using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ClingTo.Models
{
    public class Order : BaseEntity
    {
        public DateTime CompletedOn { get; set; }

        public virtual Cart Cart { get; set; }

        public virtual Customer Customer { get; set; }
    }
}