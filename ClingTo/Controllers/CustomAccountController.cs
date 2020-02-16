using ClingTo.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClingTo.Controllers
{
    public partial class AccountController
    {
        [Authorize(Roles = "Administrator")]
        public ActionResult AddUserToRole()
        {
            AddUserToRole model = new AddUserToRole();
            model.roles = new List<string>() { "Customer", "Designer", "Administrator" };
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult AddUserToRole(AddUserToRole model)
        {
            var email = model.Email;
            var user = UserManager.FindByEmail(model.Email);
            if (user == null)
                throw new HttpException(404, "There is no user with email: " + model.Email);

            UserManager.AddToRole(user.Id, model.selectedRole);
            return RedirectToAction("Index", "Home");
        }
    }
}