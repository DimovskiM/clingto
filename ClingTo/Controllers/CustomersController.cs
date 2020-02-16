using ClingTo.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Net;
using System.Web;
using Microsoft.AspNet.Identity.Owin;

namespace ClingTo.Controllers
{
    [Authorize]
    [RoutePrefix("Customers")]
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _dbContext = new ApplicationDbContext();

        public CustomersController()
        {

        }

        [HttpGet]
        [Route("AddToCart")]
        public ActionResult AddToCart(Guid productUid)
        {
            ApplicationUserManager _userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = _userManager.FindByName(User.Identity.GetUserName());
            Guid userId;
            Guid.TryParse(user.Id, out userId);

            Customer customer = _dbContext.Customers.Where(x => x.Uid == userId).FirstOrDefault();

            if (customer == null)
            {
                customer = new Customer()
                {
                    Uid = userId,
                    Requests = new List<Request>(),
                    Orders = new List<Order>()
                };
            }

            if (customer.Carts == null)
            {
                customer.Carts = new List<Cart>();
                customer.Carts.Add(new Cart()
                {
                    Products = new List<Product>(),
                    TotalPrice = 0m,
                    Uid = Guid.NewGuid()
                });
            }

            Cart cart = customer.Carts.OrderByDescending(x => x.Id).FirstOrDefault();

            Product product = _dbContext.Products.Where(x => x.Uid == productUid).FirstOrDefault();

            if (product == null)
            {
                throw new ArgumentNullException("product", $"Product with uid {productUid} does not exist");
            }

            cart.Products.Add(product);
            cart.UpdateTotalPrice();

            _dbContext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("GetCart")]
        public ActionResult ShowCart()
        {
            ApplicationUserManager _userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = _userManager.FindByName(User.Identity.GetUserName());
            Guid userId;
            Guid.TryParse(user.Id, out userId);

            Customer customer = _dbContext.Customers.Where(x => x.Uid == userId).FirstOrDefault();

            Cart cart = customer.Carts.OrderByDescending(x => x.Id).FirstOrDefault();

            ViewBag.Cart = cart;

            return View();
        }

        [HttpGet]
        [Route("CustomerDetails")]
        public ActionResult CustomerDetails()
        {
            ApplicationUserManager _userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = _userManager.FindByName(User.Identity.GetUserName());
            Guid userId;
            Guid.TryParse(user.Id, out userId);

            Customer customer = _dbContext.Customers.Where(x => x.Uid == userId).FirstOrDefault();

            return View(customer);
        }

        [HttpPost]
        [Route("CustomerDetails")]
        public ActionResult CustomerDetails(Customer customer)
        {
            ApplicationUserManager _userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = _userManager.FindByName(User.Identity.GetUserName());
            Guid userId;
            Guid.TryParse(user.Id, out userId);

            customer.Uid = userId;

            _dbContext.Entry(customer).State = System.Data.Entity.EntityState.Modified;
            _dbContext.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("Charge")]
        public ActionResult Charge()
        {

            ApplicationUserManager _userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = _userManager.FindByName(User.Identity.GetUserName());
            Guid userId;
            Guid.TryParse(user.Id, out userId);

            Customer customer = _dbContext.Customers.Where(x => x.Uid == userId).FirstOrDefault();

            Cart cart = _dbContext.Carts.OrderBy(x => x.Id).FirstOrDefault();

            return View(new StripeChargeModel
            {
                Amount = (int)cart.TotalPrice
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Charge(StripeChargeModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string chargeId = await ProcessPayment(model);

            ApplicationUserManager _userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var user = _userManager.FindByName(User.Identity.GetUserName());
            Guid userId;
            Guid.TryParse(user.Id, out userId);

            Customer customer = _dbContext.Customers.Where(x => x.Uid == userId).FirstOrDefault();

            Cart cart = _dbContext.Carts.OrderBy(x => x.Id).FirstOrDefault();

            customer.Orders.Add(new Order
            {
                CompletedOn = DateTime.Now,
                Uid = Guid.NewGuid(),
                Customer = customer,
                Cart = cart,
            });

            customer.Carts.Add(new Models.Cart
            {
                Products = new List<Product>(),
                TotalPrice = 0m,
                Uid = Guid.NewGuid()
            });

            _dbContext.SaveChanges();

            return RedirectToAction("Index", "Orders");
        }

        private async Task<string> ProcessPayment(StripeChargeModel model)
        {
            return await Task.Run(() =>
            {
                var charge = new Stripe.StripeChargeCreateOptions
                {
                    
                    Amount = (int)(model.Amount),
                    Currency = "usd",
                    Description = "Purchase",
                    Source = new Stripe.StripeSourceOptions
                    {
                        TokenId = model.Token
                    }
                };

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;

                var chargeService = new Stripe.StripeChargeService("sk_test_4lknJTkG0q14upuDDfpZO1Nl006QJSGsZF");
                Stripe.StripeCharge chargeId = null;
                try
                {
                    chargeId = chargeService.Create(charge);
                } catch (Exception e)
                {
                    System.Diagnostics.Debug.Print(e.Message);
                }


                return chargeId.Id;
           });
        }
    }
}