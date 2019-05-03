using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult AddToCart(int id)
        {
            ShopingCartEntities db = new ShopingCartEntities();
            var product = db.Products.Where(x => x.Id == id).First();
            Cart cart = new Cart();
            cart.ProductName = product.ProductName;

            db.Carts.Add(cart);
            db.SaveChanges();
            string message = "Product Added to Cart!";
            return RedirectToAction("Index", "Home", new { Message = message });
        }

        public ActionResult ViewProducts()
        {
            ShopingCartEntities db = new ShopingCartEntities();
            var List = db.Products.ToList();
            List<ProductViewModel> PassList = new List<ProductViewModel>();
            foreach (var i in List)
            {
                ProductViewModel p = new ProductViewModel();
                p.Id = i.Id;
                p.Name = i.ProductName;
                
                p.Price = Convert.ToInt32(i.ProductPrice);
                p.ImagePath = i.ImagePath;
                PassList.Add(p);
            }
            return View(PassList);
        }

        public ActionResult ViewCart()
        {
            ShopingCartEntities db = new ShopingCartEntities();
            var list = db.Carts.ToList();
            List<CartViewModel> PassList = new List<CartViewModel>();
            foreach(var i in list)
            {
                CartViewModel c = new CartViewModel();
                c.Name = i.ProductName;

                PassList.Add(c);

            }
            return View(PassList);
        }

        public ActionResult Index(string message)
        {
            ViewBag.Message = message;
            return View();
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