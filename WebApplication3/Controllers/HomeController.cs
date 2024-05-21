using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication3.Models;
using Microsoft.AspNetCore.Http;


namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            List<ProductTable> products = ProductTable.GetAllProducts();

            int? userID = _httpContextAccessor.HttpContext.Session.GetInt32("UserID");
            

            ViewData["Products"] = products;
            ViewData["UserID"] = userID;
            ViewData["SignUpSuccess"] = TempData["SignUpSuccess"];
            ViewData["LoginSuccess"] = TempData["LoginSuccess"];

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }

        public IActionResult MyWork()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        public IActionResult Transactions()
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
