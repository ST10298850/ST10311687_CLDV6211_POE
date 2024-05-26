using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApplication3.Models;
using Microsoft.AspNetCore.Http;


namespace WebApplication3.Controllers
{
    public class CartController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            int? userID = _httpContextAccessor.HttpContext.Session.GetInt32("UserID");

            if (!userID.HasValue)
            {
                // User is not logged in, redirect to login page
                return RedirectToAction("Login", "Home");
            }

            var cartItems = CartModel.GetCartItems(userID.Value);
            ViewData["CartItem"] = cartItems;
            return View("~/Views/Home/Cart.cshtml", cartItems);
        }

        [HttpPost]
        public IActionResult AddToCart(int productID, int quantity)
        {
            int? userID = _httpContextAccessor.HttpContext.Session.GetInt32("UserID");
            if (userID.HasValue)
            {
                CartModel.AddToCart(userID.Value, productID, quantity); // Modify the AddToCart method to take a quantity parameter
                return RedirectToAction("Index", "Cart");
            }
            else
            {
                // User is not logged in, redirect to login page
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpPost]
        public IActionResult ClearCart()
        {
            int? userID = _httpContextAccessor.HttpContext.Session.GetInt32("UserID");
            if (userID.HasValue)
            {
                CartModel.ClearCart(userID.Value);
                return RedirectToAction("Index", "Cart");
            }
            else
            {
                // User is not logged in, redirect to login page
                return RedirectToAction("Login", "Home");
            }
        }
    }
}

