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

            var products = _dbContext.Products.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(x => x.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "title_desc":
                    products.OrderByDescending(x => x.Name);
                    break;
                case "price_desc":
                    products.OrderByDescending(x => x.Price);
                    break;
                case "Price":
                    products.OrderBy(x => x.Price);
                    break;
                default:
                    products.OrderBy(x => x.Name);
                    break;
            }

            int pageSize = 3;
            int pageNumber = page ?? 1;

            return View(products.ToArray().ToPagedList(pageNumber, pageSize));
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