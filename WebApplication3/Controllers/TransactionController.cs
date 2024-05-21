using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class TransactionController : Controller
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        public TransactionController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }



        [HttpPost]
        public ActionResult PlaceOrder(int userID, int productID)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ProductTable.con_string))
                {
                    string sql = "INSERT INTO transactionTable (userID, productID) VALUES (@UserID, @ProductID)";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userID);
                        cmd.Parameters.AddWithValue("@ProductID", productID);

                        con.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        con.Close();

                        if (rowsAffected > 0)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            return View("OrderFailed");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                var errorModel = new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    Message = ex.Message,
                    Exception = ex
                };
                return View("Error", errorModel);
            }
        }

        [HttpGet]
        public ActionResult ViewTransactions()
        {
           int? userID = _httpContextAccessor.HttpContext.Session.GetInt32("UserID"); 
            if (!userID.HasValue || userID.Value == 0)
            {
                var errorModel = new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    Message = "Invalid user ID.",
                    Exception = new ArgumentNullException(nameof(userID))
                };
                return View("Error", errorModel);
            }

            List<TransactionViewModel> transactions = new List<TransactionViewModel>();

            try
            {
                using (SqlConnection con = new SqlConnection(ProductTable.con_string))
                {
                    string sql = "SELECT pt.Name, pt.Price FROM transactionTable tt INNER JOIN ProductTable pt ON tt.productID = pt.ID WHERE tt.userID = @UserID";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userID.Value);
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            TransactionViewModel transaction = new TransactionViewModel
                            {
                                ProductName = reader["Name"].ToString(),
                                ProductPrice = Convert.ToDecimal(reader["Price"])
                            };
                            transactions.Add(transaction);
                        }
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                var errorModel = new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    Message = ex.Message,
                    Exception = ex
                };
                return View("Error", errorModel);
            }

            return View(transactions);
        }
    }
}







