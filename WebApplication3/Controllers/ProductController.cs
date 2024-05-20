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
            var result = products.InsertProduct(products);
            if (result > 0)
            {
                // You can add some logging or success handling here
                TempData["InsertSuccess"] = "Product added successfully.";
            }
            else
            {
                // You can add some error handling here
                TempData["InsertError"] = "Product insertion failed.";
            }
            return RedirectToAction("Index", "Home");
        }

        //[HttpPost]
        //public ActionResult MyWork(ProductTable products)
        //{ 
        //    var result2 = prodtbl1.insert_product(products);            
        //    return RedirectToAction("Index", "Home");
        //}

        //[HttpGet]
        //public ActionResult MyWork()
        //{
        //    return View(prodtbl1);
        //}
    }
}
