using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class ProductController : Controller
    {
        public ProductTable prodtbl1 = new ProductTable();


        [HttpPost]
        public ActionResult MyWork(ProductTable products)
        { 
            var result2 = prodtbl1.insert_product(products);            
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult MyWork()
        {
            return View(prodtbl1);
        }
    }
}
