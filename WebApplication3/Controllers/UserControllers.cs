using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;
using Microsoft.Extensions.Logging;

namespace WebApplication3.Controllers
{
    public class UserControllers : Controller
    {
        private readonly ILogger<UserControllers> _logger;

        public UserControllers(ILogger<UserControllers> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public ActionResult SignUp(Table_1 user)
        {
            _logger.LogInformation("Received sign-up request with user data: {@user}", user);

            if (ModelState.IsValid)
            {
                var usrtbl = new Table_1();
                var result = usrtbl.InsertUser(user);
                if (result > 0)
                {
                    TempData["SignUpSuccess"] = "You have signed up successfully!";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["SignUpError"] = "Sign up failed. Please try again.";
                }
            }
            else
            {
                TempData["SignUpError"] = "Invalid data. Please check the input and try again.";
            }

            return RedirectToAction("SignUp");
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }
    }
}


