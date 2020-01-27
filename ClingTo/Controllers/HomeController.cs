using ClingTo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace ClingTo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _dbContext = new ApplicationDbContext();

        public ActionResult Index(string sortOrder, string currentFilter, string searchString,
                                  int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TitleSortParam = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.PriceSortParam = sortOrder == "Price" ? "price_desc" : "Price";

            if (searchString != null)
            {
                page = 1;
            } else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var products = _dbContext.Products.ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(x => x.Name.Contains(searchString)).ToList();
            }

            switch (sortOrder)
            {
                case "title_desc":
                    products = products.OrderByDescending(x => x.Name).ToList();
                    break;
                case "price_desc":
                    products = products.OrderByDescending(x => x.Price).ToList();
                    break;
                case "Price":
                    products = products.OrderBy(x => x.Price).ToList();
                    break;
                default:
                    products = products.OrderBy(x => x.Name).ToList();
                    break;
            }

            int pageSize = 5;
            int pageNumber = page ?? 1;

            return View(products.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}