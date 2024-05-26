//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Data.SqlClient;
//using System.Collections.Generic;
//using WebApplication3.Models;
//using Microsoft.AspNetCore.Http;
//using System.Diagnostics;

//namespace WebApplication3.Controllers
//{
//    public class TransactionController : Controller
//    {
//        private readonly IHttpContextAccessor _httpContextAccessor;

//        public TransactionController(IHttpContextAccessor httpContextAccessor)
//        {
//            _httpContextAccessor = httpContextAccessor;
//        }

//        [HttpPost]
//        public ActionResult PlaceOrder(int productID, int quantity)
//        {
//            int? userID = _httpContextAccessor.HttpContext?.Session?.GetInt32("UserID");

//            if (!userID.HasValue || userID.Value == 0)
//            {
//                var errorModel = new ErrorViewModel
//                {
//                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
//                    Message = "Invalid user ID.",
//                    Exception = new ArgumentNullException(nameof(userID))
//                };
//                return View("Error", errorModel);
//            }

//            try
//            {
//                using (SqlConnection con = new SqlConnection(ProductTable.con_string))
//                {
//                    string sql = "SELECT ProductID, Quantity FROM Cart WHERE UserID = @UserID";
//                    List<CartModel> cartItems = new List<CartModel>();

//                    using (SqlCommand cmd = new SqlCommand(sql, con))
//                    {
//                        cmd.Parameters.AddWithValue("@UserID", userID);
//                        con.Open();
//                        SqlDataReader reader = cmd.ExecuteReader();
//                        while (reader.Read())
//                        {
//                            CartModel item = new CartModel
//                            {
//                                ProductID = Convert.ToInt32(reader["ProductID"]),
//                                Quantity = Convert.ToInt32(reader["Quantity"])
//                            };
//                            cartItems.Add(item);
//                        }
//                        con.Close();
//                    }

//                    foreach (var item in cartItems)
//                    {
//                        sql = "INSERT INTO transactionTable (userID, productID, Quantity) VALUES (@UserID, @ProductID, @Quantity)";
//                        using (SqlCommand cmd = new SqlCommand(sql, con))
//                        {
//                            cmd.Parameters.AddWithValue("@UserID", userID);
//                            cmd.Parameters.AddWithValue("@ProductID", item.ProductID);
//                            cmd.Parameters.AddWithValue("@Quantity", item.Quantity);
//                            con.Open();
//                            cmd.ExecuteNonQuery();
//                            con.Close();
//                        }
//                    }
//                }

//                return RedirectToAction("Transactions", "Home");
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.Message);
//                var errorModel = new ErrorViewModel
//                {
//                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
//                    Message = ex.Message,
//                    Exception = ex
//                };
//                return View("Error", errorModel);
//            }
//        }

//        [HttpGet]
//        public ActionResult ViewTransactions()
//        {
//            int? userID = _httpContextAccessor.HttpContext?.Session?.GetInt32("UserID");

//            if (!userID.HasValue || userID.Value == 0)
//            {
//                var errorModel = new ErrorViewModel
//                {
//                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
//                    Message = "Invalid user ID.",
//                    Exception = new ArgumentNullException(nameof(userID))
//                };
//                return View("Error", errorModel);
//            }

//            List<TransactionViewModel> transactions = new List<TransactionViewModel>();

//            try
//            {
//                using (SqlConnection con = new SqlConnection(ProductTable.con_string))
//                {
//                    string sql = "SELECT tt.productID, pt.Name, pt.Price, tt.Quantity FROM transactionTable tt INNER JOIN ProductTable pt ON tt.productID = pt.ProductID WHERE tt.userID = @UserID";
//                    using (SqlCommand cmd = new SqlCommand(sql, con))
//                    {
//                        cmd.Parameters.AddWithValue("@UserID", userID.Value);
//                        con.Open();
//                        SqlDataReader reader = cmd.ExecuteReader();
//                        while (reader.Read())
//                        {
//                            TransactionViewModel transaction = new TransactionViewModel
//                            {
//                                ProductID = Convert.ToInt32(reader["productID"]),
//                                ProductName = reader["Name"]?.ToString(),
//                                ProductPrice = Convert.ToDecimal(reader["Price"]),
//                                Quantity = Convert.ToInt32(reader["Quantity"])
//                            };
//                            transactions.Add(transaction);
//                        }
//                        con.Close();
//                    }
//                }

//                Console.WriteLine($"Retrieved {transactions.Count} transactions for user {userID.Value}");

//                return View("Transactions", transactions);
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.Message);
//                var errorModel = new ErrorViewModel
//                {
//                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
//                    Message = ex.Message,
//                    Exception = ex
//                };
//                return View("Error", errorModel);
//            }
//        }
//    }
//}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using WebApplication3.Models;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

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
        public ActionResult PlaceOrder(int productID, int quantity)
        {
            int? userID = _httpContextAccessor.HttpContext?.Session?.GetInt32("UserID");

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

            try
            {
                using (SqlConnection con = new SqlConnection(ProductTable.con_string))
                {
                    con.Open();
                    string sql = "SELECT ProductID, Quantity FROM Cart WHERE UserID = @UserID";
                    List<CartModel> cartItems = new List<CartModel>();

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userID);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                CartModel item = new CartModel
                                {
                                    ProductID = Convert.ToInt32(reader["ProductID"]),
                                    Quantity = Convert.ToInt32(reader["Quantity"])
                                };
                                cartItems.Add(item);
                            }
                        }
                    }

                    foreach (var item in cartItems)
                    {
                        string insertSql = "INSERT INTO transactionTable (userID, productID, Quantity) VALUES (@UserID, @ProductID, @Quantity)";
                        using (SqlCommand cmd = new SqlCommand(insertSql, con))
                        {
                            cmd.Parameters.AddWithValue("@UserID", userID);
                            cmd.Parameters.AddWithValue("@ProductID", item.ProductID);
                            cmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                return RedirectToAction("Transactions", "Home");
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
            int? userID = _httpContextAccessor.HttpContext?.Session?.GetInt32("UserID");

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
                    con.Open();
                    string sql = "SELECT tt.productID, pt.Name, pt.Price, tt.Quantity FROM transactionTable tt INNER JOIN ProductTable pt ON tt.productID = pt.ID WHERE tt.userID = @UserID";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userID.Value);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TransactionViewModel transaction = new TransactionViewModel
                                {
                                    ProductID = Convert.ToInt32(reader["productID"]),
                                    ProductName = reader["Name"]?.ToString(),
                                    ProductPrice = Convert.ToDecimal(reader["Price"]),
                                    Quantity = Convert.ToInt32(reader["Quantity"])
                                };
                                transactions.Add(transaction);
                            }
                        }
                    }
                }

                Console.WriteLine($"Retrieved {transactions.Count} transactions for user {userID.Value}");
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

            return View("Transactions", transactions);
        }
    }
}

