using Microsoft.AspNetCore.Mvc;
using Mvc_day2.Models;
using System.Diagnostics;


namespace Mvc_day2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            CookieOptions cookieOptions= new CookieOptions();
            cookieOptions.Expires = DateTimeOffset.Now.AddDays(1);
            //Set Session & Get Session
            
            HttpContext.Session.SetString("Name", "Mohamed Ashraf");
            HttpContext.Response.Cookies.Append("User", "Mohamed.I",cookieOptions);
            HttpContext.Session.SetInt32("Pass", 12345678);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}