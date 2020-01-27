using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClingTo.Models
{
    public class AddUserToRole
    {
        [Required]
        public string Email { get; set; }

        public List<string> roles { get; set; }

        public string selectedRole { get; set; }
    }
}