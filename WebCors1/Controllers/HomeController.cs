using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebCors1.Models;

namespace WebCors1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public class User
        {
            public User(string name, int age, DateTime myProperty)
            {
                Name = name;
                Age = age;
                CreateDate = myProperty;
            }

            public string Name { get; set; }
            public int Age { get; set; }
            public DateTime CreateDate { get; set; }
        }

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [EnableCors("Policy_1")]
        public IActionResult Index()
        {
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
        [EnableCors("Policy_1")]
        public JsonResult GetUser()
        {
            User user = new User("Hi",22,new DateTime(DateTime.UtcNow.Day));
            return Json(user);
        }
    }
}