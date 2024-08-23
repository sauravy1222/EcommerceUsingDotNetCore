using EcommerceUsingDotNetCore.Data;
using EcommerceUsingDotNetCore.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace EcommerceUsingDotNetCore.Controllers
{
    public class AuthController : Controller
    { 
        
        private readonly ApplicationDbContext db;
        public AuthController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [AcceptVerbs("post","Get")]
        public IActionResult CheckExistingId(string email)
        {
            var data = db.users.Where(x=>x.Email==email).SingleOrDefault();
            if (data != null) {

              return Json($"Email {email} is already used");
            }
            else
            {
                return Json(true);
            }

        }

        public static string EncryptedPassword(string Password)
        {
            if (String.IsNullOrEmpty(Password))
            {
                return null;

            }
            else
            {
                byte[]  pass=ASCIIEncoding.ASCII.GetBytes(Password);
                string encrpass= Convert.ToBase64String(pass);
                return encrpass;
            }
        }
        public static string DecryptedPassword(string Password)
        {
            if (String.IsNullOrEmpty(Password))
            {
                return null;

            }
            else
            {
                byte[] pass = Convert.FromBase64String(Password);
                string decrpass = ASCIIEncoding.ASCII.GetString(pass);
                return decrpass;
            }
        }



        [HttpPost]
        public IActionResult SignUp(User u) {

            u.Password = EncryptedPassword(u.Password);
            u.Role = "User";
            db.users.Add(u);
            db.SaveChanges();
            return RedirectToAction("SignIn");
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignIn(Login log)
        {
            var data = db.users.Where(x => x.Email.Equals(log.Email)).SingleOrDefault();
            if (data != null)
            {      
                
                bool us=data.Email.Equals(log.Email) && DecryptedPassword(data.Password).Equals(log.Password);
                if (us) {
                    HttpContext.Session.SetString("myuser",data.Email);
                    return RedirectToAction("Index", "Product");
                
                }
            }
            else
            {
                TempData["ErrorEmail"] = "invalid Email";
                
            }
            return View();


        }

        public IActionResult AllUser()
        {

            var data =db.users.ToList();
            return Json(data);
        }
        public IActionResult Logout()

        {
            HttpContext.SignOutAsync();
            HttpContext.Session.Clear();
            return RedirectToAction("SignIn");
        }
    }
}
