using Project.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class AdminController : Controller
    {

        public ActionResult LogOut()
        {
            HelperClass.UserId = 0;
            HelperClass.Userlogged = null;
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel Collection)
        {
            ShopingCartEntities db = new ShopingCartEntities();
            var admin = db.Admins.First();
            if (admin.AdminUserName == Collection.UserName && admin.AdminPassword == Collection.Password)
            {
                HelperClass.Userlogged = "Admin";
                HelperClass.UserId = Convert.ToInt32(admin.Id);

                return RedirectToAction("Account");

            }
            return View();
        }

        public ActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult AddProduct(ProductViewModel collection)
        {
            ShopingCartEntities db = new ShopingCartEntities();
            Product product = new Product();
            product.ProductName = collection.Name;
            product.ProductPrice = collection.Price;
            product.ProductQuantity = collection.Quantity;

            if (collection.Image != null)
            {
                string filename = Path.GetFileNameWithoutExtension(collection.Image.FileName);
                string ext = Path.GetExtension(collection.Image.FileName);
                filename = filename + DateTime.Now.Millisecond.ToString();
                filename = filename + ext;
                string filetodb = "/img/Shop/" + filename;
                filename = Path.Combine(Server.MapPath("~/img/Hostels"), filename);
                collection.Image.SaveAs(filename);
                collection.ImagePath = filetodb;
            }
            else
            {
                collection.ImagePath = "/img/Shop/1.jpg";
            }
            product.ImagePath = collection.ImagePath;

            db.Products.Add(product);
            db.SaveChanges();
            return RedirectToAction("ViewProducts");
        }

        public ActionResult ViewProducts()
        {
            ShopingCartEntities db = new ShopingCartEntities();
            var List = db.Products.ToList();
            List<ProductViewModel> PassList = new List<ProductViewModel>();
            foreach(var i in List)
            {
                ProductViewModel p = new ProductViewModel();
                p.Id = i.Id;
                p.Name = i.ProductName;
                p.Quantity = Convert.ToInt32(i.ProductQuantity);
                p.Price = Convert.ToInt32(i.ProductPrice);
                p.ImagePath = i.ImagePath;
                PassList.Add(p);
            }
            return View(PassList);
        }

        public ActionResult UpdateProduct(int id)
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult UpdateProduct(int id,ProductViewModel collection)
        {
            ShopingCartEntities db = new ShopingCartEntities();
            var product = db.Products.Where(x => x.Id == id).First();
            if (collection.Name == null)
            {
                collection.Name = product.ProductName;
            }
            if (collection.Price == 0)
            {
                collection.Price = Convert.ToInt16(product.ProductPrice);
            }
            if (collection.Quantity == 0)
            {
                collection.Quantity = Convert.ToInt16(product.ProductQuantity);
            }
           
            product.ProductName = collection.Name;
            product.ProductPrice =Convert.ToInt32(collection.Price);
            product.ProductQuantity = Convert.ToInt32(collection.Quantity);

            db.SaveChanges();
            string message = "Product Updated!";
            return RedirectToAction("Account", "Admin", new { Message = message });
            
        }

        public ActionResult DeleteProduct(int id)
        {
            ShopingCartEntities db = new ShopingCartEntities();
            var ent = db.Products.Where(x => x.Id == id).First();
            db.Entry(ent).State = System.Data.Entity.EntityState.Deleted;

            db.SaveChanges();
            string message = "Product Deleted!";
            return RedirectToAction("Account", "Admin", new { Message = message });
        }
        public ActionResult Account(string message)
        {
            ShopingCartEntities db = new ShopingCartEntities();
            var admin = db.Admins.First();

            ViewBag.Name = admin.AdminName;
            ViewBag.Contact = admin.AdminContact;
            ViewBag.Email = admin.AdminEmail;
            ViewBag.Message = message;
            return View();
        }


        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Admin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Admin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
