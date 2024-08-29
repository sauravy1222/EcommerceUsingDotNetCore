using EcommerceUsingDotNetCore.Data;
using EcommerceUsingDotNetCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceUsingDotNetCore.Controllers
{
    public class ProductController : Controller
    {
        private IWebHostEnvironment env;
        private readonly ApplicationDbContext db;
        public ProductController(ApplicationDbContext db, IWebHostEnvironment env)
        {
            this.env = env;
            this.db = db;
        }
        public IActionResult Index()
        {
            
                var data = db.products.Take(5).ToList();
                return View(data);
          


        }
        [HttpPost]
        public IActionResult Index( string choice)
        {
            if(choice=="All")
            {
                var data =db.products.ToList();
                return View(data);
            }
            else if (choice == "LTH")
            {

                var data = db.products.OrderBy(x => x.Price).ToList();
                return View(data);
            }
            else
            {
                var data = db.products. Take(5).ToList();
                return View(data) ;
            }

           
        }
        public IActionResult AddProduct()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddProduct(ProductViewModal pm)
        {

            var path = env.WebRootPath;
            var filePath = "Content/Images/" + pm.Picture.FileName;
            var fullPath = Path.Combine(path, filePath);
            UploadFile(pm.Picture, fullPath);
            var obj = new Product()
            {
                Pname = pm.Pname,
                Pcat = pm.Pcat,
                Price = pm.Price,
                Picture = filePath

            };
            db.Add(obj);
            db.SaveChanges();
            TempData["msg"] = "Product Added Successfully";
            return RedirectToAction("Index");

        }
        public void UploadFile(IFormFile file, string path)
        {
            FileStream stream = new FileStream(path, FileMode.Create);
            file.CopyTo(stream);

        }

        //public IActionResult LTH()
        //{
        //    var data =db.products.OrderBy(x=>x.Price).ToList(); 
        //    return View(data);
        //}


        public IActionResult AddToCart(int id)
        {
            string sess = HttpContext.Session.GetString("myuser");
            var prod = db.products.Find(id);
            var obj = new Cart()
            {
                Pname = prod.Pname,
                Pcat = prod.Pcat,
                Picture = prod.Picture,
                Price = prod.Price,
                Suser = sess


            };
            db.carts.Add(obj);
            db.SaveChanges();
            return RedirectToAction("ViewCart");


        }
        public IActionResult ViewCart()
        {
            string sess = HttpContext.Session.GetString("myuser");
            var data = db.carts.Where(X => X.Suser.Equals(sess)).ToList();
            return View(data);

        }

       [HttpPost]
public IActionResult DeleteToCart(int id)
{
    var data = db.carts.Find(id);
    if (data != null)
    {
        db.carts.Remove(data);
        db.SaveChanges();
    }
    return RedirectToAction("ViewCart");
}



    }
}
