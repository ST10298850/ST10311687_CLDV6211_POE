using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace WebApplication3.Models
{
    public class CartModel
    {
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }

        public static string con_string = ProductTable.con_string;

        public static void AddToCart(int userID, int productID, int quantity)
        {
            using (SqlConnection con = new SqlConnection(con_string))
            {
                string sql = "IF EXISTS (SELECT * FROM Cart WHERE UserID = @UserID AND ProductID = @ProductID) " +
                             "UPDATE Cart SET Quantity = Quantity + @Quantity WHERE UserID = @UserID AND ProductID = @ProductID " +
                             "ELSE INSERT INTO Cart (UserID, ProductID, Quantity) VALUES (@UserID, @ProductID, @Quantity)";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    cmd.Parameters.AddWithValue("@ProductID", productID);
                    cmd.Parameters.AddWithValue("@Quantity", quantity);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static List<CartModel> GetCartItems(int userID)
        {
            List<CartModel> cartItems = new List<CartModel>();

            using (SqlConnection con = new SqlConnection(con_string))
            {
                string sql = "SELECT Cart.ProductID, ProductTable.Name AS ProductName, ProductTable.Price AS ProductPrice, Cart.Quantity " +
                             "FROM Cart " +
                             "JOIN ProductTable ON Cart.ProductID = ProductTable.ID " +
                             "WHERE Cart.UserID = @UserID";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@UserID", userID);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        CartModel item = new CartModel
                        {
                            ProductID = Convert.ToInt32(reader["ProductID"]),
                            ProductName = reader["ProductName"].ToString(),
                            ProductPrice = Convert.ToDecimal(reader["ProductPrice"]),
                            Quantity = Convert.ToInt32(reader["Quantity"])
                        };
                        cartItems.Add(item);
                    }
                }
            }

            return cartItems;
        }

        public static void ClearCart(int userID)
        {
            using (SqlConnection con = new SqlConnection(con_string))
            {
                string sql = "DELETE FROM Cart WHERE UserID = @UserID";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@UserID", userID);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}


