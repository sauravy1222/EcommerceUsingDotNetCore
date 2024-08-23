using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceUsingDotNetCore.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            string suser = HttpContext.Session.GetString("myuser");

            return View();
        }

        public IActionResult About()
        {
            return View();


        }

    }
}
