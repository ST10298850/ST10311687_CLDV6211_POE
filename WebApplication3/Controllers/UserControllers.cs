using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class UserControllers : Controller
    {
       public Table_1 usrtbl = new Table_1();

        [HttpPost]

        public ActionResult About(Table_1 Users)
        {
            var result = usrtbl.insert_User(Users);
            return RedirectToAction("Index", "Home");

        }
        [HttpPost]

        public ActionResult About()
        {
            return View(usrtbl);
        }
    }
}
